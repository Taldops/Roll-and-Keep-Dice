using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darken : MonoBehaviour {

	//TODO this can be made more flexible, like shrink script
	//TODO use generics/templates to combine the two into one script?

	public float animLength;

	private float progress = 0;
	private Color target_color;
	private Color original_color;
	private Renderer rend;


	public void set_brightness(float brightness)
	{
		brightness = Mathf.Clamp01(brightness);
		target_color = new Color(brightness * original_color.r, brightness * original_color.g, brightness * original_color.b, original_color.a);
		progress = 0;
	}

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		original_color = rend.material.color;
		target_color = original_color;
		progress = 1;
	}
	
	// Update is called once per frame
	void Update () {
		progress += Time.deltaTime/animLength;
		Color c = Color.Lerp(original_color, target_color, progress);
		rend.material.color = c;//
	}
}
