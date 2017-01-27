/*
 * Edited version of the script by Dave Hampson 
 * from http://wiki.unity3d.com/index.php/FramesPerSecond
 */

using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	public float size;

	float deltaTime = 0.0f;

	void Start()
	{
		size = Mathf.Max(size, 1);
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		//int w = Mathf.RoundToInt(Screen.width * size), h = Mathf.RoundToInt(Screen.height * size);
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = Mathf.RoundToInt(size * h * 0.02f);
		style.normal.textColor = new Color (0.2f, 9.0f, 0.2f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
			
}