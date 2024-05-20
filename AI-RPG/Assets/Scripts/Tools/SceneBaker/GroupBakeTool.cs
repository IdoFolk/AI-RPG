using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Linq;



#if UNITY_EDITOR

public class GroupBakeTool : OdinMenuEditorWindow {

	[HideInInspector]
	public string sceneGroupsDatabasePath = "Assets/DataBase/Core/GroupBakeTool/SceneGroups/";

	[HideInInspector]
	public string prefix = "Scene Group_";

	OdinMenuTree CurrentMenuTree;

	///inspector parameters related to "BakeParameters" - preserved in case we want it back
	//[HideInInspector]
	//public string BakeParametersDatabasePath = "Assets/DataBase/Core/GroupBakeTool/BakeParameters/";
	//#endregion
	//[TabGroup("Bake")]
	//[SerializeField]
	//[HideLabel, InlineEditor (Expanded = true)]
	//private BakeParameters bakeParameters;

	[BoxGroup ("Group Bake Tool", CenterLabel = true)]


	[ReadOnly]
	[SerializeField]
	[LabelText(text:" ")]
	[ListDrawerSettings (HideRemoveButton = true, HideAddButton = true, ShowFoldout = false, DraggableItems = false,ShowItemCount = false)]
	private List<int> inspectorcheat;

	[Title("Select Groups To Bake",TitleAlignment = TitleAlignments.Centered,Bold = true,HorizontalLine = false)]
	[LabelText(text:" ")]
	[ListDrawerSettings(HideRemoveButton =true,HideAddButton = true,ShowFoldout = false,DraggableItems = false, ShowItemCount = false)]
	[SerializeField]
	List<ToggleSceneGroup> toggleSceneGroups = new List<ToggleSceneGroup> ();

	[HideInInspector]
	[ReadOnly]
	[SerializeField]
	private List<SceneGroup> chachedSceneGroups = new List<SceneGroup>();

	[System.Serializable]
	class ToggleSceneGroup {
		public ToggleSceneGroup(string newGroupName ,bool newToggle) {
			groupName = newGroupName;
			toggle = newToggle;
		}

		string groupName;
		public string getGroupName => groupName;

		bool toggle;
		public bool getIsToggled => toggle;

		[GUIColor("getToggleColor")]
		[Button(Name = "$getGroupName")]
		void Toggle () {
			toggle = !toggle;
		}

		Color getToggleColor => toggle ? Color.white : Color.white * 0.5f;
	}

	[OnInspectorGUI]
	void FillAllSceneGroupsToDic () {
		for (int i = 0; i < CurrentMenuTree.MenuItems[1].ChildMenuItems.Count; i++) {
			if (i == 0)
				continue;

			SceneGroup sceneGroup = (SceneGroup)CurrentMenuTree.MenuItems[1].ChildMenuItems[i].Value;

			if(!chachedSceneGroups.Contains(sceneGroup))
			chachedSceneGroups.Add (sceneGroup);

			if(!toggleSceneGroups.Any(t => t.getGroupName == sceneGroup.name)) {
				toggleSceneGroups.Add (new ToggleSceneGroup (sceneGroup.name, false));
			}
			
		}
	}


	//[BoxGroup ("Group Bake Tool", CenterLabel = true)]
	//[SerializeField]
	//private bool render = true;

	//[BoxGroup ("Group Bake Tool", CenterLabel = true)]
	//[SerializeField]
	//private bool renderLightProbes = true;
	
	//[BoxGroup ("Group Bake Tool", CenterLabel = true)]
	//[SerializeField]
	//private bool renderReflectionProbes = true;
	
	private IEnumerator progressFunc;
	private bool loaded = false;
	private List<string> preBakecachedScenePaths = new List<string>();
	
	[BoxGroup ("Group Bake Tool", CenterLabel = true,Order = 0)]
	[Button(ButtonSizes.Large, ButtonStyle.CompactBox)]
	private void Bake () {
		// Cache all open scenes to reopen after bake 
		preBakecachedScenePaths.Clear();

		for (int i = 0; i < EditorSceneManager.sceneCount; i++) {
			preBakecachedScenePaths.Add(EditorSceneManager.GetSceneAt (i).path);
		}
				
		InitializeBake ();
	}

	#region OdinMenuTree
	//opens the Menu Tree
	[MenuItem ("Tools/GroupBakeTool")]
	static private void OpenWindow () {
		GetWindow<GroupBakeTool> ().Show ();
	}

	// Tree structure 
	protected override OdinMenuTree BuildMenuTree () {
		var tree = new OdinMenuTree () {
			{"Main",this,EditorIcons.House}
			
		};

		CurrentMenuTree = tree;

		tree.Selection.SupportsMultiSelect = false;
		tree.Add ("Scene Groups/Create New", new SceneGroupCreator(this),EditorIcons.Plus);

		tree.AddAllAssetsAtPath ("Scene Groups", sceneGroupsDatabasePath,typeof(SceneGroup));

		var treeaa = new OdinMenuTree () {
			{"Main",this,EditorIcons.House }
		};

		tree.MenuItems[1].AddIcon(SdfIconType.List);
		for (int i = 0; i < tree.MenuItems[1].ChildMenuItems.Count; i++) {
			if (i == 0)
				continue;

			tree.MenuItems[1].ChildMenuItems[i].Name = tree.MenuItems[1].ChildMenuItems[i].Name.Replace (prefix, "");
		}
		
		///"BakeParameters Related"
		//tree.Add ("Bake Parameters/Create New", new BakeParametersCreator (this),EditorIcons.Plus);
		//tree.AddAllAssetsAtPath ("Bake Parameters", BakeParametersDatabasePath, typeof(BakeParameters));

		return tree;
	}

	// used to add Delete button when required 
	protected override void OnBeginDrawEditors () {
		OdinMenuTreeSelection selected = this.MenuTree.Selection;

		SirenixEditorGUI.BeginHorizontalToolbar (); {
			GUILayout.FlexibleSpace ();

			if (selected.SelectedValue is SceneGroup || selected.SelectedValue is BakeParameters) {
				GUIContent toolBarContent = new GUIContent ("Delete Current");

				if (SirenixEditorGUI.ToolbarButton (toolBarContent)) {
					string path = null;

					if(selected.SelectedValue is SceneGroup) {
						SceneGroup asset = selected.SelectedValue as SceneGroup;
						 path = AssetDatabase.GetAssetPath (asset);
					}

					if (selected.SelectedValue is BakeParameters) {
						BakeParameters asset = selected.SelectedValue as BakeParameters;
						path = AssetDatabase.GetAssetPath (asset);
					}

					if (path != null) {
						AssetDatabase.DeleteAsset (path);
						AssetDatabase.SaveAssets ();
					}
				}
			}
			

		}

		SirenixEditorGUI.EndHorizontalToolbar ();
	}



	#endregion

	#region Asset Creators
	//Base asset creating functionalities 
	public abstract class AssetCreator {
		protected GroupBakeTool groupBakeTool;

		protected void CreateNewAsset (Object asset, string path, string assetName, string extension) {
			AssetDatabase.CreateAsset (asset, path + assetName + extension);
			AssetDatabase.SaveAssets ();
		}

	}

	public class SceneGroupCreator : AssetCreator {
		public SceneGroupCreator (GroupBakeTool _groupBakeTool) {
			groupBakeTool = _groupBakeTool;
			// opens new SO for edition before saving
			newGroup = ScriptableObject.CreateInstance<SceneGroup> ();
			newGroup.groupName = "newGroup";
		}

		[TabGroup ("Scene Groups")]
		[HideLabel, InlineEditor (Expanded = true)]
		public SceneGroup newGroup;

		[TabGroup ("Scene Groups")]
		[Button ("Create New Scene Group")]
		private void CreateNew () {
			CreateNewAsset (newGroup, groupBakeTool.sceneGroupsDatabasePath, groupBakeTool.prefix + newGroup.groupName, ".asset");
			
		}
	}

	///"BakeParameters Related"

	//public class BakeParametersCreator : AssetCreator {
	//	public BakeParametersCreator (GroupBakeTool _groupBakeTool) {
	//		groupBakeTool = _groupBakeTool;
	//		// opens new SO for edition before saving
	//		bakeParameters = ScriptableObject.CreateInstance<BakeParameters> ();
	//		bakeParameters.paramatersName = "newParameters";
	//	}
	//	[TabGroup ("Bake Parameters")]
	//	[HideLabel, InlineEditor (Expanded = true)]
	//	public BakeParameters bakeParameters;

	//	[TabGroup ("Bake Parameters")]
	//	[Button ("Create New Bake Parameters")]
	//	private void CreateNew () {
	//		CreateNewAsset (bakeParameters, groupBakeTool.BakeParametersDatabasePath, bakeParameters.paramatersName, ".asset");
	//	}
	//}

	#endregion

	#region Bake
	/// <summary>
	/// setting all the required data for baking 
	/// </summary>
	void InitializeBake () {
		EditorSceneManager.sceneOpened += OnSceneOpened;

		List<SceneGroup> sceneGroupsToBake = new List<SceneGroup>();

		foreach(ToggleSceneGroup toggleSceneGroup in toggleSceneGroups) {
			if (toggleSceneGroup.getIsToggled)
				sceneGroupsToBake.Add (chachedSceneGroups.Find (t => t.groupName == toggleSceneGroup.getGroupName));
		}

		progressFunc = BatchBakeSceneGroups (sceneGroupsToBake.ToArray());

		EditorApplication.update += OnBatchBakeUpdate;
	}
	

	void OnBakeComplete () {
		Debug.Log (bakeCyclesComplete);
		bakeCyclesComplete++;
	}

	float bakeCyclesComplete = 0;
	private IEnumerator BatchBakeSceneGroups (SceneGroup[] SceneGroupsArray) {
		//Unload all Opned Scens 
		for (int i = EditorSceneManager.sceneCount - 1; i >= 0; i--) {
			Scene scene = EditorSceneManager.GetSceneAt (i);

			AsyncOperation ao = EditorSceneManager.UnloadSceneAsync (scene);
			yield return ao;
		}

		foreach (SceneGroup group in SceneGroupsArray) {
			yield return new WaitForSeconds (1);
			//Convert SceneGroups to useable data
			Object[] SceneGroupAsObjects = ConvertSceneFieldArrayToObjectArray (group.scenes.ToArray ());

			string[] SceneGroupAsStrings = ConvertSceneObjArrayToStringArray (SceneGroupAsObjects);

			//Load Group
			bool firstInGroup = true;
			foreach (string sceneString in SceneGroupAsStrings) {
				loaded = false;

				if (firstInGroup) {
					firstInGroup = false;
					EditorSceneManager.OpenScene (sceneString, OpenSceneMode.Single);
				} else {
					EditorSceneManager.OpenScene (sceneString, OpenSceneMode.Additive);
				}

				while (!loaded) {
					Debug.Log ("LoadingScene");
					yield return null;
				}

				Debug.Log ("Scene: " + sceneString + "has been loaded");

				yield return new WaitForSeconds (0.5f);
			}

			Debug.Log ("Done loading group:" + group.groupName);

			yield return new WaitForSeconds (1f);

			int cycleCount = SceneGroupAsStrings.Length;
			bakeCyclesComplete = 0;

			Lightmapping.bakeCompleted += OnBakeComplete;

			Lightmapping.Bake ();

			yield return new WaitUntil (() => bakeCyclesComplete == cycleCount);

			Lightmapping.bakeCompleted -= OnBakeComplete;

			EditorSceneManager.MarkAllScenesDirty ();
			EditorSceneManager.SaveOpenScenes ();

			yield return null;

			//Unload all 
			for (int i = EditorSceneManager.sceneCount - 1; i >= 0; i--) {
				Scene scene = EditorSceneManager.GetSceneAt (i);

				AsyncOperation ao = EditorSceneManager.UnloadSceneAsync (scene);
				yield return ao;
			}

			yield return null;
		}

		//Load All Open 1PreCached Scenes
		bool firstInPreGroup = true;
		foreach (string found in preBakecachedScenePaths) {
			loaded = false;

			if (firstInPreGroup) {
				firstInPreGroup = false;
				EditorSceneManager.OpenScene (found, OpenSceneMode.Single);
			} else {
				EditorSceneManager.OpenScene (found, OpenSceneMode.Additive);
			}

			while (!loaded) {
				Debug.Log ("LoadingScene");
				yield return null;
			}

			Debug.Log ("Scene: " + found + "has been loaded");
		}

		Debug.Log ("Batch bake finished");
	}

	//iterates the IEnumerator with no need of monobehaviour
	private void OnBatchBakeUpdate () {
		if (progressFunc.MoveNext ())
			return;

		EditorApplication.update -= OnBatchBakeUpdate;
	}

	private void OnSceneOpened (Scene scene, OpenSceneMode mode) {
		loaded = true;
	}

	private string[] ConvertSceneObjArrayToStringArray (Object[] scenesAsObjList) {
		string[] sceneList = new string[scenesAsObjList.Length];

		for (int i = 0; i < scenesAsObjList.Length; i++) {
			var path = AssetDatabase.GetAssetPath (scenesAsObjList[i]);
			sceneList[i] = path;
		}

		return sceneList;
	}

	private Object[] ConvertSceneFieldArrayToObjectArray (SceneField[] scenesAsFieldsList) {
		Object[] scenesAsObjectsList = new Object[scenesAsFieldsList.Length];

		for (int i = 0; i < scenesAsFieldsList.Length; i++) {
			Object obj = scenesAsFieldsList[i].getObject;
			scenesAsObjectsList[i] = obj;
		}

		return scenesAsObjectsList;
	}
	#endregion


	#region IndividualBatchBaking 
	/// <summary>
	/// iterates each scene from the list then opening them as individuals and baking them with
	/// the bake settings named as "ftRendererLightmap"
	/// </summary>
	/// <returns></returns>
	//private IEnumerator BatchBakeAsIndividuals () {

	//	for (int i = 0; i < sceneList.Length; i++) {
	//		loaded = false;
	//		EditorSceneManager.OpenScene (sceneList[i]);
	//		while (!loaded)
	//			yield return null;

	//		var storage = ftRenderLightmap.FindRenderSettingsStorage ();
	//		var bakery = ftRenderLightmap.instance != null ? ftRenderLightmap.instance : new ftRenderLightmap ();
	//		bakery.LoadRenderSettings ();

	//		if (_render) {
	//			bakery.RenderButton (false);
	//			while (ftRenderLightmap.bakeInProgress) {
	//				yield return null;
	//			}
	//		}

	//           if (_renderLightProbes) {
	//			bakery.RenderLightProbesButton (false);
	//			while (ftRenderLightmap.bakeInProgress) {
	//				yield return null;
	//			}
	//		}

	//		if (_renderReflectionProbes) {
	//			bakery.RenderReflectionProbesButton (false);
	//			while (ftRenderLightmap.bakeInProgress) {
	//				yield return null;
	//			}
	//		}

	//		EditorSceneManager.MarkAllScenesDirty ();
	//		EditorSceneManager.SaveOpenScenes ();

	//		yield return null;
	//	}

	//	Debug.Log ("Batch bake finished");
	//}
	#endregion
}

#endif