using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBG : MonoBehaviour {

	public GameObject BG;
	public float distance;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = Mathf.Atan(Mathf.Deg2Rad * GetComponent<Camera>().fieldOfView) * Vector3.Distance(transform.position, BG.transform.position) * 2.15f;
		float y = (9.0f/16.0f) * x;
		BG.transform.localScale = new Vector3(x, 0, y);
		BG.transform.up = -transform.forward;
		BG.transform.position = transform.position + transform.forward * distance;
	}
}
