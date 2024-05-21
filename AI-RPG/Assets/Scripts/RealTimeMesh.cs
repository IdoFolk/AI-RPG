using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RealTimeMesh : MonoBehaviour
{
	private MeshRenderer objectMeshRenderer;
	private MeshFilter objectMeshFilter;
	private Mesh objectMesh;


    private Vector3[] modifiedMeshVertices;
    

    // Start is called before the first frame update
    void Start()
    {
		objectMeshRenderer = GetComponent<MeshRenderer>();
        objectMeshFilter = GetComponent<MeshFilter>();
        objectMesh = objectMeshFilter.mesh;

		//ModifyMesh ();

    }

    [Button]
    void ModifyMesh () {
        if (!Application.isPlaying)
            return;

        modifiedMeshVertices = getCurrentVerticesPositionsArray ();
        int triangleCounter = 0;
        int vertexCounter = 0;

		for (int i = 0; i < modifiedMeshVertices.Length; i++) {
            if(vertexCounter % 3 == 0) {
                triangleCounter++;
            }

            
            modifiedMeshVertices[i] +=  objectMesh.normals[i].normalized * (0.1f * triangleCounter);

			vertexCounter++;

		}

        objectMesh.vertices = modifiedMeshVertices;

	}

	[Button]
	void SliceMesh () {
		if (!Application.isPlaying)
			return;

		Vector3[][] splittedVertices = SplitCurrentVericesToMultipleArrays ();

		objectMesh.Clear ();
		
		objectMesh.vertices = splittedVertices[0];

		//Vector3[] triangles = new Vector3[splittedVertices[0].Length - 2];
		//for (int i = 0; i < splittedVertices[0].Length - 2; i++) {

		//	triangles[i] = new 
		//}

		objectMesh.triangles = DrawFilledTriangles(splittedVertices[0]);
	
		objectMesh.RecalculateBounds ();
		objectMesh.RecalculateTangents ();
		objectMesh.RecalculateNormals ();
	}

	int[] DrawFilledTriangles (Vector3[] points) {
		int triangleAmount = points.Length - 2;
		List<int> newTriangles = new List<int> ();
		for (int i = 0; i < triangleAmount; i++) {

			newTriangles.Add (i==0 ? 0 :i - 1);
			newTriangles.Add (i);
			newTriangles.Add (i + 1);
		}
		return newTriangles.ToArray ();
	}

	Vector3[][] SplitCurrentVericesToMultipleArrays () {
		modifiedMeshVertices = getCurrentVerticesPositionsArray ();
		int triangleCounter = 0;
		//int vertexCounter = 0;
		int iterationCount = 2;
		

		Vector3 cutByPlaneCenter = transform.position;
		Vector3[][] splittedVeticesArray = new Vector3[iterationCount][];

		
		Vector3[] iterationDirections = new Vector3[iterationCount];
		iterationDirections[0] = new Vector3 (0, 1, 0);
		iterationDirections[1] = new Vector3 (0, -1, 0);


		for (int x = 0; x < iterationCount; x++) {
			Vector3[] iterationVertices = new Vector3[modifiedMeshVertices.Length];
			int vertCounter = 0;

			float hightDirection = iterationDirections[x].y;

			for (int i = 0; i < modifiedMeshVertices.Length; i++) {
				
				


				float yDistanceFromCenter = modifiedMeshVertices[i].y;

				if ((hightDirection > 0 && yDistanceFromCenter > 0) || (hightDirection < 0 && yDistanceFromCenter < 0)) {
					iterationVertices[vertCounter] = modifiedMeshVertices[i];
					vertCounter++;
				} else {
					continue;	
				}

				Vector3 oppositDirection = new Vector3 (0, hightDirection, 0);

				//modifiedMeshVertices[i] += objectMesh.normals[i].normalized * (0.1f * triangleCounter);
				modifiedMeshVertices[i] += oppositDirection * 1;

				//vertexCounter++;

			}

			splittedVeticesArray[x] = new Vector3[vertCounter];

			for (int i = 0; i < vertCounter; i++) {
				splittedVeticesArray[x][i] = iterationVertices[i];
			}
		}

		Debug.Log (splittedVeticesArray[0].Length);
		Debug.Log (splittedVeticesArray[1].Length);

		return splittedVeticesArray;
	}


	Vector3[] getCurrentVerticesPositionsArray () {
        Vector3[] currentMeshVertices = new Vector3[objectMesh.vertices.Length];
        
		for (int i = 0; i < objectMesh.vertices.Length; i++) {
			//Debug.Log (objectMesh.vertices[i].x);
			Vector3 vertPosition = new Vector3 (objectMesh.vertices[i].x, objectMesh.vertices[i].y, objectMesh.vertices[i].z);

			currentMeshVertices[i] = vertPosition;

		}

        return currentMeshVertices;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
