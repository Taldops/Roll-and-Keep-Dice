/*
 * This script is for changing models depending on the quality
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshQuality : MonoBehaviour {

	public Mesh[] meshes;
	public Mesh[] collisions;

	//Updates all GameObjects with this script
	public void UpdateAll()		//It could be static, but then it doesn't work with GUI
	{
		MeshQuality[] objects = Object.FindObjectsOfType<MeshQuality>();
		print(objects.Length);
		foreach(MeshQuality o in objects)
		{
			o.UpdateMesh();
		}
	}


	// Use this for initialization
	void Start () {
		UpdateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateMesh()		//TODO Make this more flexible
	{
		switch (QualitySettings.GetQualityLevel())
		{
		default:
		case 0:
			GetComponent<MeshFilter>().mesh = meshes[0];
			GetComponent<MeshCollider>().sharedMesh = meshes[0];
			GetComponent<Light>().range = 0;
			break;
		case 1:
		case 2:
			GetComponent<MeshFilter>().mesh = meshes[1];
			GetComponent<MeshCollider>().sharedMesh = meshes[0];
			GetComponent<Light>().range = 0;
			break;
		case 3:
			GetComponent<MeshFilter>().mesh = meshes[2];
			GetComponent<MeshCollider>().sharedMesh = meshes[1];
			GetComponent<Light>().range = 4;
			break;
		}
	}
}
