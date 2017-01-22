using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Vector3 table_center;

	// Use this for initialization
	void Start () {
		transform.LookAt(table_center + new Vector3(transform.position.x, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
