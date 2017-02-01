using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMoving : MonoBehaviour {

	//TODO Maybe check 3 consecutive positions?

	public bool stillMoving
	{
		get
		{
			return false;
		}
	}

	public float stillTime;		//Duration of not moving to be considered still
	public float threshold;		// Maximum deviation to be considered not moved

	private Vector3 lastPosition;
	private Quaternion lastRotation;
	private bool moving = false;
	private float timestamp;	//time of last check/update

	// Use this for initialization
	void Awaken () {
		lastPosition = transform.position;
		lastRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - timestamp >= stillTime)
		{
			timestamp = Time.time;
			//TODO use ternary operator
			if(Vector3.Distance(transform.position, lastPosition) < threshold
				&& Quaternion.Angle(transform.rotation, lastRotation) < threshold)
			{
				moving = false;
			}
			else
			{
				moving = true;
			}

			lastPosition = transform.position;
			lastRotation = transform.rotation;
		}
	}
}
