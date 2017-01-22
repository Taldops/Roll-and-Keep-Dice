using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DiceManager : MonoBehaviour {
	
	public GameObject die_prefab;
	
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
			if(die.GetComponent<Die>().value == 0 || die.GetComponent<Die>().rolling)		//TODO Add velocity check
			{
				result = true;
			}
			else
			{
				//Stops dice from jittering		//TODO Make StopJittering.cs
				die.GetComponent<Rigidbody>().drag = 8;
				die.GetComponent<Rigidbody>().angularDrag = 0.2f;
			}
		}
		return result;
	}

	public void clear_dice()
	{
		foreach(GameObject die in all_dice)
		{
			//die.GetComponent<VanishParticles>().play();
			GameObject.Destroy(die);
		}
		all_dice.Clear();
	}

}

