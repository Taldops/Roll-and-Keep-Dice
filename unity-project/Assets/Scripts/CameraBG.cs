using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBG : MonoBehaviour{

	//TODO Make bigger Background to fit 1920 x 1200

	public GameObject BG;
	public float distance;

	private Vector3 rotation = Vector3.zero;
	private float distance_modifier = 2.12f;	//makes sure the background fills the screen exactly

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{	
		float current_dm = distance_modifier;
		//if(orientation == DeviceOrientation.Portrait)
		if(Screen.orientation == ScreenOrientation.Portrait && Screen.width < Screen.height)
		{
			rotation = new Vector3(0, 90, 0);
			BG.GetComponentInChildren<TextureScroll>().scroll_vector = new Vector2(0, BG.GetComponentInChildren<TextureScroll>().scroll_vector.magnitude);
			current_dm *= 0.62f;
		}
		//else if(orientation == DeviceOrientation.LandscapeLeft)
		else
		{
			rotation = new Vector3(0, 0, 0);
			BG.GetComponentInChildren<TextureScroll>().scroll_vector = new Vector2(BG.GetComponentInChildren<TextureScroll>().scroll_vector.magnitude, 0);
		}

		//Caclulations
		float x = Mathf.Atan(Mathf.Deg2Rad * GetComponent<Camera>().fieldOfView) * Vector3.Distance(transform.position, BG.transform.position) * current_dm;
		float y = (9.0f/16.0f) * x;
		BG.transform.localScale = new Vector3(x, 0, y);
		if(true)//if(BG.transform.up != - transform.forward)
		{
			BG.transform.up = -transform.forward;
			BG.transform.Rotate(rotation);
		}
		BG.transform.position = transform.position + transform.forward * distance;
	}
}
