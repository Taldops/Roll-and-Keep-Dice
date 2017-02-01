using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlacement : MonoBehaviour {

	public GameObject GUIPort;
	public GameObject GUILand;

	public GameObject cam;

	public float cam_offset;

	private Vector3 cam_pos_neutral;

	// Use this for initialization
	void Start () //Why does this have to be start, not awaken
	{		
		cam_pos_neutral = cam.transform.position;
	}

	// Update is called once per frame
	void Update () {
		Vector3 offset = Vector3.zero;
		if(Screen.orientation == ScreenOrientation.Portrait && Screen.width * 0.75f < Screen.height)
		{
			offset = cam_pos_neutral + new Vector3 (0, 20, -cam_offset * 2);
			GUIPort.SetActive(true);
			GUILand.SetActive(false);
		}
		else
		{
			offset = cam_pos_neutral + new Vector3 (cam_offset, 0, 0);
			GUIPort.SetActive(false);
			GUILand.SetActive(true);
		}
		cam.transform.position = cam_pos_neutral + offset;
		cam.transform.LookAt(offset);
	}
}
