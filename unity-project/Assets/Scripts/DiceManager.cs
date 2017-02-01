using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DiceManager : MonoBehaviour {
	
	public GameObject die_prefab;
	public float movementThreshold;		//When is a die considered "still rolling"
	
	private static List<GameObject> all_dice;
	
	void Start () {
		all_dice = new List<GameObject>();
		//die_prefab.SetActive(false);
	}

	public int count_active()
	{
		return all_dice.Count;
	}
	
	//Creates and returns an active die
	public GameObject roll_die(Vector3 position, bool[] crit_list)
	{
		GameObject die = Instantiate(die_prefab, position, Quaternion.identity);
		die.name = "Die " + all_dice.Count;
		die.SetActive(true);
		die.GetComponent<SuccessEffects>().set_successes(crit_list);
		all_dice.Add(die);
		return die;
	}
	
	//true when there are dice still rolling, rolling is checked using rigidBody.velocity and rigidBody.angularVelocity
	public bool still_rolling()
	{
		bool result = false;
		foreach(GameObject die in all_dice)
		{
			if(die.GetComponent<Die>().value == 0 // || die.GetComponent<Die>().rolling)		//TODO Add velocity check; Still occaisonal error without?
				|| die.GetComponent<Rigidbody>().angularVelocity.sqrMagnitude > movementThreshold
				|| die.GetComponent<Rigidbody>().velocity.sqrMagnitude > movementThreshold
				|| die.GetComponent<NotMoving>().stillMoving)
			{
				result = true;
			}
			else
			{
				//Stops dice from jittering		//TODO Make StopJittering.cs
				die.GetComponent<Rigidbody>().drag = 10;
				die.GetComponent<Rigidbody>().angularDrag = 0.5f;
			}
		}
		return result;
	}

	public void clear_dice()
	{
		foreach(GameObject die in all_dice)
		{
			//die.GetComponent<VanishParticles>().play();
			die.GetComponent<ShrinkScript>().destroy_on_small = true;
			die.GetComponent<ShrinkScript>().play();
			//GameObject.Destroy(die);
		}
		all_dice.Clear();
	}

}

