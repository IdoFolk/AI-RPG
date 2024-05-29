using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using NUnit.Framework;



#if UNITY_EDITOR

public class DataCreator : OdinMenuEditorWindow {

	[HideInInspector]
	public string resourcesDatabasePath = "Assets/Data/Resources/";

	[HideInInspector]
	public string abilitiesDatabasePath = "Assets/Data/Abilities/";

	[HideInInspector]
	public string itemsDatabasePath = "Assets/Data/Items/";
	
	[HideInInspector]
	public string prefix = "Data_";

	OdinMenuTree CurrentMenuTree;
	
	private IEnumerator progressFunc;
	private bool loaded = false;
	private List<string> preBakecachedScenePaths = new List<string>();
	
	[BoxGroup ("Data Creator", CenterLabel = true,Order = 0)]
	[Button(ButtonSizes.Large, ButtonStyle.CompactBox)]
	private void Bake () {
		// Cache all open scenes to reopen after bake 			
		InitializeBake ();
	}

	#region OdinMenuTree
	//opens the Menu Tree
	[MenuItem ("Tools/Data Creator")]
	static private void OpenWindow () {
		GetWindow<DataCreator> ().Show ();
	}

	// Tree structure 
	protected override OdinMenuTree BuildMenuTree () {
		var tree = new OdinMenuTree () {
			{"Main",this,EditorIcons.House}
			
		};

		CurrentMenuTree = tree;

		tree.Selection.SupportsMultiSelect = false;
		tree.Add ("Datas Creators/Create Resource", new DataAssetCreator(this,"Data_Resource"),EditorIcons.Plus);
		tree.Add ("Datas Creators/Create Ability", new DataAssetCreator (this,"Data_Ability"), EditorIcons.Plus);
		tree.Add ("Datas Creators/Create Item", new DataAssetCreator (this, "Data_Item"), EditorIcons.Plus);

		tree.AddAllAssetsAtPath ("Datas/Resources", resourcesDatabasePath,typeof(DataCreator_Data));
		tree.AddAllAssetsAtPath ("Datas/Abilities", abilitiesDatabasePath, typeof (DataCreator_Data));
		tree.AddAllAssetsAtPath ("Datas/Items", itemsDatabasePath, typeof (DataCreator_Data));

		var treeaa = new OdinMenuTree () {
			{"Main",this,EditorIcons.House }
		};

		if (tree != null && tree.MenuItems.Count > 0) {
			if (tree.MenuItems.Count > 1)
				tree.MenuItems[1].AddIcon (SdfIconType.List);

			if (tree.MenuItems.Count > 2) {

				tree.MenuItems[2].AddIcon (SdfIconType.List);

				if (tree.MenuItems[2].ChildMenuItems != null)
					for (int i = 0; i < tree.MenuItems[2].ChildMenuItems.Count; i++) {
						tree.MenuItems[2].ChildMenuItems[i].AddIcon (SdfIconType.ListNested);
					}
			}

		}
		
		for (int i = 0; i < tree.MenuItems[1].ChildMenuItems.Count; i++) {
			if (i == 0)
				continue;
			//prefix for all datas
			//tree.MenuItems[1].ChildMenuItems[i].Name = tree.MenuItems[1].ChildMenuItems[i].Name.Replace (prefix, "");
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

			if (selected.SelectedValue is DataCreator_Data) {
				GUIContent toolBarContent = new GUIContent ("Delete Current");

				if (SirenixEditorGUI.ToolbarButton (toolBarContent)) {
					string path = null;

					if(selected.SelectedValue is DataCreator_Data) {
						DataCreator_Data asset = selected.SelectedValue as DataCreator_Data;
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
		protected DataCreator dataCreator;

		protected void CreateNewAsset (Object asset, string path, string assetName, string extension) {
			AssetDatabase.CreateAsset (asset, path + assetName + extension);
			AssetDatabase.SaveAssets ();
		}

	}

	public class DataAssetCreator : AssetCreator {
		public DataAssetCreator (DataCreator _dataCreator,string _dataType) {
			dataCreator = _dataCreator;
			dataType = _dataType;
			// opens new SO for edition before saving

			switch (dataType) {
				case "Data_Resource":
					newData = ScriptableObject.CreateInstance<Data_Resource> ();
					break;

				case "Data_Ability":
					newData = ScriptableObject.CreateInstance<Data_Ability_Basic> ();
					break;

				case "Data_Item":
					newData = ScriptableObject.CreateInstance<Data_Item> ();
					break;

				default:
					break;
			}
			
			
			newData.Name = "newData";
		}

		[TabGroup ("Datas")]
		[HideLabel, InlineEditor (Expanded = true)]
		public DataCreator_Data newData;

		string dataType;

		[TabGroup ("Datas")]
		[Button ("Create new DataGroup")]
		private void CreateNew () {
			string targetPath = string.Empty;
			
			switch (dataType) {
				case "Data_Resource":
					targetPath = dataCreator.resourcesDatabasePath;
					break;

				case "Data_Ability":
					targetPath = dataCreator.abilitiesDatabasePath;
					break;

				case "Data_Item":
					targetPath = dataCreator.itemsDatabasePath;
					break;


				default:
					break;
			}

			if (targetPath == string.Empty)
				return;

			CreateNewAsset (newData, targetPath, dataCreator.prefix + newData.Name, ".asset");
			
		}
	}

	
	#endregion

	#region Bake
	/// <summary>
	/// setting all the required data for baking 
	/// </summary>
	void InitializeBake () {
		progressFunc = BatchBakeSceneGroups ();

		EditorApplication.update += OnBatchBakeUpdate;
	}
	

	private IEnumerator BatchBakeSceneGroups () {
		yield return null;
	}

	//iterates the IEnumerator with no need of monobehaviour
	private void OnBatchBakeUpdate () {
		if (progressFunc.MoveNext ())
			return;

		EditorApplication.update -= OnBatchBakeUpdate;
	}


	#endregion

}

#endif