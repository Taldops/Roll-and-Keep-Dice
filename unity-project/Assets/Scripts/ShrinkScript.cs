using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkScript : MonoBehaviour {

	public float ShrinkTime;
	public bool destroy_on_small;

	private Vector3 original_scale;
	private Vector3 target_scale;

	private float progress = 0;
	private bool active = false;	//currently changing size

	public void set_target_factor(Vector3 tf)
	{
		target_scale = original_scale;
		target_scale.Scale(tf);
	}

	// Use this for initialization
	void Start () {
		original_scale = transform.localScale;
		print(target_scale);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(destroy_on_small && transform.localScale.sqrMagnitude < 0.05f)	//This can be done better
		{
			print("destroying");
			GameObject.Destroy(this);
		}
		if(active)
		{
			if (progress >= 1)
			{
				active = false;
			}
			progress += (1/ShrinkTime) * Time.deltaTime;		//TODO USE Time.deltaTime more (in darken)
			transform.localScale = Vector3.Lerp(original_scale, target_scale, progress);
		}
	}

	public void shrink()
	{
		if(ShrinkTime <= 0)
		{
			ShrinkTime = 0.01f;
		}
		active = true;
	}

	public void revert()
	{
		if(ShrinkTime <= 0)
		{
			ShrinkTime = 0.01f;
		}
		progress = 1 - progress;
		Vector3 temp = original_scale;
		original_scale = target_scale;
		target_scale = temp;
		active = true;
	}

}
