using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkScript : MonoBehaviour {

	//TODO diable gravity or make kinematic while shrinking?

	public bool start_on_active;
	public float ShrinkTime;
	public bool destroy_on_small;
	public bool pulse; 		//revert immediately
	public bool loop;		//loops if pulse is true

	private Vector3 original_scale;
	[SerializeField] private Vector3 target_scale;		//PUT A MULTIPLIER WHEN YOU SET THIS IN THE EDITOR
	private bool direction = false; 	//Keeps track of direction. false = change ; true = back to normal

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
		if(start_on_active)		//TODO Should this be in awaken?
		{
			play();
		}
		set_target_factor(target_scale);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(destroy_on_small && transform.localScale.sqrMagnitude < 0.001f)	//This can be done better
		{
			GameObject.Destroy(this.gameObject);
		}
		if(active)
		{
			if (progress >= 1)
			{
				active = false;
				if(direction)		//TODO This needs refactoring
				{
					direction = false;
					progress = 0;
					Vector3 temp = original_scale;
					original_scale = target_scale;
					target_scale = temp;
					transform.localScale = original_scale;
				}
				else if(pulse || loop)
				{
					revert();
				}
			}
			progress += Time.deltaTime/ShrinkTime;
			transform.localScale = Vector3.Lerp(original_scale, target_scale, progress);
		}

		if(GetComponent<MeshCollider>() != null && GetComponent<Rigidbody>() != null)	//TODO This can be done better
		{
			//Disable Stuff when too small:		//TODO Refactor
			if(transform.localScale.sqrMagnitude < 0.001f)
			{
				GetComponent<MeshCollider>().enabled = false;
				GetComponent<Rigidbody>().useGravity = false;
			}
			else
			{
				GetComponent<MeshCollider>().enabled = true;
				GetComponent<Rigidbody>().useGravity = true;
			}
		}
	}

	public void play()
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
		direction = !direction;
		progress = 1 - progress;
		Vector3 temp = original_scale;
		original_scale = target_scale;
		target_scale = temp;
		active = true;
	}

}
