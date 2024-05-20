using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

public class grassManager : MonoBehaviour
{
    [SerializeField]
    GrassFan fan;

	[SerializeField]
	float fanAngle;

	[SerializeField]
    Renderer[] grassObjectRenderers;

	[SerializeField]
	Mesh gizmoMesh;

	[SerializeField]
	int vectorFieldGridRes;

	[SerializeField]
	bool ShowField;

	[SerializeField]
	bool ShowVectors;

	Material grassMaterial;
    // Start is called before the first frame update

    MaterialPropertyBlock propertyBlock;

	Dictionary<Renderer, grassProperties> windPerRenderer = new Dictionary<Renderer, grassProperties> ();
    void Start()
    {
        propertyBlock = new MaterialPropertyBlock ();

		foreach(Renderer renderer in grassObjectRenderers) {
			windPerRenderer.Add (renderer, new grassProperties());
		}

		if (fanColliderVF == null)
			fanColliderVF = new ColliderVectorField (fan.getWindCollider, vectorFieldGridRes);

		if(grassObjectRenderers.Length > 0)
		grassObjectRenderers[0].GetPropertyBlock (propertyBlock);
	}

    // Update is called once per frame
    void Update()
    {
		if (fanColliderVF == null)
			return;
		Profiler.BeginSample ("VF");
		UpdateColliderVF ();
		Profiler.EndSample ();

		Profiler.BeginSample ("updateGrass");
		UpdateDirectionToFan ();
		Profiler.EndSample ();
	}

	void UpdateColliderVF () {
		
		fanColliderVF.CalculatePositions ();

		gridWorldPositions = fanColliderVF.getWorldPositions;
	}

	float angle360Output;
	float angle360 (Vector3 from, Vector3 to, Vector3 right) {
		angle360Output = Vector3.Angle (from, to);
		return (Vector3.Angle (right, to) > 90f) ? 360f - angle360Output : angle360Output;
	}

	Vector3 fanDir;
	Vector3 windDirection;
	bool isAffectedByFan;

	//int effectedCount;
	void UpdateDirectionToFan () {
	//	effectedCount = 0;

		if (grassObjectRenderers.Length > 0) {
			foreach (Renderer renderer in grassObjectRenderers) {
				fanDir = (renderer.transform.position - fan.transform.position).normalized;
				windDirection = fanDir;

				//float distanceFromFan = Vector3.Distance (fan.transform.position, renderer.transform.position);

				//Vector3 localPosFromFan = fanDir * distanceFromFan;

				//Vector3 localToFanPos = fan.transform.InverseTransformPoint (renderer.transform.position);

				//Profiler.BeginSample ("inFanCheck");
				isAffectedByFan = TransformBoxContainsPoint (fan.transform, fan.getWindCollider.center, fan.getWindCollider.size, renderer.transform.position);
				//Profiler.EndSample ();

				if (isAffectedByFan && fanColliderVF != null)
					windDirection = GetVectorFieldVectorByWorldPosition (fanColliderVF, renderer.transform.position);

				if (windDirection == Vector3.zero)
					isAffectedByFan = false;

				if (isAffectedByFan) {
					windPerRenderer[renderer].windAngle = Mathf.Lerp (windPerRenderer[renderer].windAngle, angle360 (renderer.transform.forward, -windDirection, renderer.transform.right), Time.deltaTime * 10);
					//effectedCount++;
					windPerRenderer[renderer].windStrength = Mathf.Clamp (Mathf.Lerp(windPerRenderer[renderer].windStrength , windDirection.magnitude * 15f , Time.deltaTime * 2), 0, 50);
				} else {
					//windAngle = windPerRenderer[renderer].windAngle;
					windPerRenderer[renderer].windStrength = Mathf.Clamp(Mathf.Lerp (windPerRenderer[renderer].windStrength, 0 , Time.deltaTime),0,50);
				}
				//windPerRenderer[renderer].windAngle = 0;
				
				propertyBlock.SetFloat ("_WindAngle", windPerRenderer[renderer].windAngle);
				
				propertyBlock.SetFloat ("_WindStrength", windPerRenderer[renderer].windStrength);

				renderer.SetPropertyBlock (propertyBlock);
			}
		}

		//Debug.Log (effectedCount);
	}

	Vector3[,,] gridWorldPositions;
	ColliderVectorField fanColliderVF;
	BoxCollider vectorFieldCollider;
	Vector3 gridPosInVF;
	Vector3 localPosToVF;
	
	Vector3 GetVectorFieldVectorByWorldPosition (ColliderVectorField colliderVF,Vector3 worldPosition) {

		//vf.CalculatePositions ();
		bool foundRelevantGridPos;
		vectorFieldCollider = colliderVF.getGridCollider;
		//gridWorldPositions = new Vector3[colliderGrid.getGridRes, colliderGrid.getGridRes, colliderGrid.getGridRes];
		//Debug.Log ("WorldPos-" + worldPosition);
		localPosToVF = vectorFieldCollider.transform.InverseTransformPoint (worldPosition);
		//Debug.Log (localPosToVF);
		for (int x = 0; x < colliderVF.getGridRes; x++) {

			//Debug.Log (x);
			//Debug.Log (((float)x * colliderVF.getGridSizes.x) - (colliderVF.getGridSizes.x * (colliderVF.getGridRes * 0.5f)));

			if ((((float)x * colliderVF.getGridSizes.x) - (colliderVF.getGridSizes.x * (colliderVF.getGridRes * 0.5f))) < (localPosToVF.x))
				continue;

			//Debug.Log (x);
			for (int y = 0; y < colliderVF.getGridRes; y++) {

				if ((((float)y * colliderVF.getGridSizes.y) - (colliderVF.getGridSizes.y * (colliderVF.getGridRes * 0.5f))) < (localPosToVF.y))
					continue;

				//Debug.Log (y);
				for (int z = 0; z < colliderVF.getGridRes; z++) {

					if ((((float)z * colliderVF.getGridSizes.z) - (colliderVF.getGridSizes.z * (colliderVF.getGridRes * 0.5f))) < (localPosToVF.z))
						continue;


					//Debug.Log ("Reach");

					//Debug.Log (gridWorldPositions[x, y, z]);
					//Profiler.BeginSample ("FinDlOCALPos");
					gridPosInVF = colliderVF.getVectorArray[x, y, z];// vectorFieldCollider.transform.InverseTransformPoint (gridWorldPositions[x, y, z]);
																	 //Profiler.EndSample ();

					//Profiler.BeginSample ("isInRelevantGridPos");
					foundRelevantGridPos = TransformBoxContainsPoint (vectorFieldCollider.transform, localPosToVF, colliderVF.getGridSizes, worldPosition);
					//Profiler.EndSample ();

					if (foundRelevantGridPos) {
					//	Debug.Log ("found");
						Profiler.BeginSample ("Noises");
						float strengthNoise = Mathf.PerlinNoise (Time.time + gridWorldPositions[x, y, z].x / colliderVF.getGridRes, Time.time + gridWorldPositions[x, y, z].z / colliderVF.getGridRes);

						float directionNoise = Mathf.PerlinNoise (Time.time + gridWorldPositions[x, y, z].z / colliderVF.getGridRes, strengthNoise);

						Profiler.EndSample ();
						return (colliderVF.getGridCollider.transform.forward * colliderVF.getGridSizes.z * strengthNoise * 0.5f + colliderVF.getGridCollider.transform.right * (directionNoise - 0.5f));
					}

					


						//Gizmos.DrawWireCube (colliderGrid.getGridCollider.transform.TransformPoint(colliderGrid.getVectorArray[x, y, z]),  new Vector3 (colliderGrid.getGridSizes.x, colliderGrid.getGridSizes.y, colliderGrid.getGridSizes.z));
					//	gridWorldPositions[x, y, z] = colliderVF.getGridCollider.transform.TransformPoint (colliderVF.getVectorArray[x, y, z]);
					//Gizmos.DrawMesh (gizmoMesh, gridWorldPositions[x, y, z], fan.transform.rotation, colliderVF.getGridSizes);
					//Gizmos.DrawMesh (gizmoMesh,colliderGrid.getVectorArray[x, y, z], fan.transform.rotation,  new Vector3 (colliderGrid.getGridSizes.x, colliderGrid.getGridSizes.y, colliderGrid.getGridSizes.z));
					//Gizmos.color = UnityEngine.Color.white;

					//float directionNoise = Mathf.PerlinNoise (Time.time + gridWorldPositions[x, y, z].x / colliderVF.getGridRes, Time.time + gridWorldPositions[x, y, z].z / colliderVF.getGridRes);
					//float strengthNoise = Mathf.PerlinNoise (Time.time + gridWorldPositions[x, y, z].z / colliderVF.getGridRes, directionNoise);
					//Vector3 vectorDirection = (colliderVF.getGridCollider.transform.forward * colliderVF.getGridSizes.z * strengthNoise * 0.5f + colliderVF.getGridCollider.transform.right * (directionNoise - 0.5f));
					//Gizmos.DrawLine (gridWorldPositions[x, y, z], gridWorldPositions[x, y, z] - vectorDirection);
					//Debug.Log (colliderGrid.getGridSizes);
				}
			}
		}

		return Vector3.zero;
	}

	
	private void OnDrawGizmos () {
		if (!ShowField && !ShowVectors)
			return;

		if(Application.isPlaying)
		ColliderGrizGizmo (fanColliderVF);
	
	}
	
	void ColliderGrizGizmo (ColliderVectorField colliderGrid) {
		if (fanColliderVF == null)
			return;


		//Profiler.BeginSample ("noise");
		
		Gizmos.color = UnityEngine.Color.green;
		for (int x = 0; x < colliderGrid.getGridRes; x++) {
			for (int y = 0; y < colliderGrid.getGridRes; y++) {
				for (int z = 0; z < colliderGrid.getGridRes; z++) {
					Gizmos.color =new UnityEngine.Color ((x * colliderGrid.getGridSizes.x) / vectorFieldCollider.size.x, (y * colliderGrid.getGridSizes.y) / vectorFieldCollider.size.y, (z * colliderGrid.getGridSizes.z) / vectorFieldCollider.size.z,0.05f);
					
					//gridWorldPositions[x,y,z] = colliderGrid.getGridCollider.transform.TransformPoint (colliderGrid.getVectorArray[x, y, z]);
					if(ShowField)
					 Gizmos.DrawMesh (gizmoMesh, gridWorldPositions[x,y,z], fan.transform.rotation,colliderGrid.getGridSizes);
					
					Gizmos.color = UnityEngine.Color.white;
					
					float directionNoise = Mathf.PerlinNoise (Time.time + gridWorldPositions[x, y, z].x / colliderGrid.getGridRes, Time.time + gridWorldPositions[x, y, z].z / colliderGrid.getGridRes);
					float strengthNoise = Mathf.PerlinNoise (Time.time + gridWorldPositions[x, y, z].z / colliderGrid.getGridRes, directionNoise);
					Vector3 vectorDirection =  (colliderGrid.getGridCollider.transform.forward * colliderGrid.getGridSizes.z * strengthNoise * 0.5f + colliderGrid.getGridCollider.transform.right * (directionNoise - 0.5f));
					
					if(ShowVectors)
					Gizmos.DrawLine (gridWorldPositions[x, y, z], gridWorldPositions[x, y, z] - vectorDirection);
					//Debug.Log (colliderGrid.getGridSizes);
				}
			}
		}
		//Profiler.EndSample();
	}

	Vector3 transformBoxlocalPos;
	public bool TransformBoxContainsPoint (Transform colliderTransform, Vector3 offset, Vector3 colliderSize, Vector3 point) {
		transformBoxlocalPos = colliderTransform.InverseTransformPoint (point);
		
		transformBoxlocalPos -= offset;

		//if(offset != Vector3.zero)
		//Debug.Log ("LocalPosAfterOffset -" + localPos);

		if (Mathf.Abs (transformBoxlocalPos.x) < (colliderSize.x * 0.5f) && Mathf.Abs (transformBoxlocalPos.y) < (colliderSize.y * 0.5f) && Mathf.Abs (transformBoxlocalPos.z) < (colliderSize.z * 0.5f))
			return true;
		else
			return false;
	}



}
class grassProperties {
	public float windStrength;
	public float windAngle;
}

public class ColliderVectorField {

	Vector3 grizSizes;
	public Vector3 getGridSizes => grizSizes;

	int gridRes;
	public int getGridRes => gridRes;


	Vector3[,,] vectorArray;
	public Vector3[,,] getVectorArray => vectorArray;


	Vector3[,,] worldPositions;
	public Vector3[,,] getWorldPositions => worldPositions;


	BoxCollider gridCollider;
	public BoxCollider getGridCollider => gridCollider;
	public ColliderVectorField (BoxCollider newGridCollider, int newGridRes) {

		gridCollider = newGridCollider;

		gridRes = newGridRes;

		worldPositions = new Vector3[gridRes, gridRes, gridRes];
		//grizSizes = new Vector3 (gridCollider.bounds.size.x / gridRes, gridCollider.bounds.size.y / gridRes, gridCollider.bounds.size.z / gridRes);

		//vectorArray = new Vector3[gridRes, gridRes, gridRes];

		CalculatePositions ();
	}

	void CalculateGridProperties () {
		
		grizSizes = new Vector3 (gridCollider.size.x / (float)gridRes, gridCollider.size.y / (float)gridRes, gridCollider.size.z / (float)gridRes);
	
		vectorArray = new Vector3[gridRes, gridRes, gridRes];
	}

	public void CalculatePositions () {

		CalculateGridProperties ();

		for (float x = 0; x < gridRes; x++) {
			for (float y = 0; y < gridRes; y++) {
				for (float z = 0; z < gridRes; z++) {
					//Vector3 cubes = new Vector3 ((float)x * grizSizes.x - ((float)gridRes * grizSizes.x), (float)y * grizSizes.y - ((float)gridRes * grizSizes.y), (float)z * grizSizes.z - ((float)gridRes * grizSizes.z));
					Vector3 cubePositions = new Vector3 ((float)x * grizSizes.x /*- ((float)gridRes * grizSizes.x)*/, (float)y * grizSizes.y /*- ((float)gridRes * grizSizes.y) */, (float)z * grizSizes.z /*- ((float)gridRes * grizSizes.z)*/);
					vectorArray[(int)x, (int)y, (int)z] = gridCollider.center - (grizSizes * 0.5f * gridRes) + grizSizes * 0.5f + cubePositions;// + cubes; // - new Vector3 ((float)gridRes * grizSizes.x, (float)gridRes * grizSizes.y, (float)gridRes * grizSizes.z);// + cubes;
					worldPositions[(int)x, (int)y, (int)z] = gridCollider.transform.TransformPoint (vectorArray[(int)x, (int)y, (int)z]);
				}
			}
		}
	}
}