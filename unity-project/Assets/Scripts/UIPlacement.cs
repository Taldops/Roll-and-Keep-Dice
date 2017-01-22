using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlacement : MonoBehaviour {

	public GameObject cam;

	public float UI_ratio;
	public float ratio_exponent;
	public float boarder;

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
		float size = 0;
		float screen_ratio = ((float)Screen.width) / ((float)Screen.height);
		float h = Mathf.Sqrt(1920 * 1080 * 1/screen_ratio);
		float w = h * screen_ratio;
		//transform.parent.GetComponent<CanvasScaler>().referenceResolution = new Vector2(w, h);
		//print(transform.parent.GetComponent<CanvasScaler>().referenceResolution);
		if(Screen.orientation == ScreenOrientation.Portrait && Screen.width * 0.75f < Screen.height)
		{
			transform.parent.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920 * 0.7f, h*0.5f);
			offset = cam_pos_neutral + new Vector3 (0, 20, -cam_offset * 2);
			GetComponent<GridLayoutGroup>().startAxis = GridLayoutGroup.Axis.Horizontal;
			GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			size = -Screen.height * (UI_ratio/Screen.width);
			GetComponent<RectTransform>().offsetMin = new Vector2(boarder, boarder);
			GetComponent<RectTransform>().offsetMax = new Vector2(-boarder, size);
			GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;

			transform.parent.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.0f;
		}
		else
		{
			transform.parent.GetComponent<CanvasScaler>().referenceResolution = new Vector2(w, 1080);
			offset = cam_pos_neutral + new Vector3 (cam_offset, 0, 0);
			GetComponent<GridLayoutGroup>().startAxis = GridLayoutGroup.Axis.Vertical;
			size = Screen.width * (UI_ratio/Screen.height);
			GetComponent<RectTransform>().offsetMin = new Vector2(size, boarder);
			GetComponent<RectTransform>().offsetMax = new Vector2(-boarder, -boarder);
			GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			transform.parent.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
		}
		cam.transform.position = cam_pos_neutral + offset;
		cam.transform.LookAt(offset);
	}
}
