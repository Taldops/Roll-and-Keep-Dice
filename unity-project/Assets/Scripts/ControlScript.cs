using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ControlScript : MonoBehaviour {

	//Physical roll parameters
	public float shake_force;
	public Vector3 Spawn_center;
	public Vector3 Throw_force;
	public float distance;
	public float die_mass;

	public GameObject GUI;

	public Material crit_material;

	//State variables
	private int Tens = 0;	//number of tens to be rerolled. 0 Mean time for a new Roll. -1 Means Roll is still in Progress
	private List<GameObject> Active_dice;
	private int current_sum = 0;	//Current sum of all rolls
	private Vector3 base_accel;
	private int critAkku = 0;

	private UIScript UI;

	DiceManager Dice;

	void Start () {
		Active_dice = new List<GameObject>();
		Dice = GetComponent<DiceManager>();
		Input.gyro.enabled = true;
		base_accel = Input.acceleration;
	}

	public void reset()
	{
		Active_dice.Clear();
		Tens = 0;
		current_sum = 0;
		UI.toggle_all(true);
		UI.update_result_display(0, Color.black, FontStyle.Normal);
		UI.switch_mode_crit(false);
		Dice.clear_dice();
		base_accel = Input.acceleration;
		critAkku = 0;
	}

	public void initiate_roll()
	{
		int to_be_rolled = UI.roll_num();
		bool crit_roll = false;
		if (Tens != 0)
		{
			for(int i = 0; i < Active_dice.Count; ++i)
			{
				if(i >= UI.keep_num())
				{
					//Active_dice[i].SetActive(false);
					Active_dice[i].GetComponent<ShrinkScript>().destroy_on_small = false;
					Active_dice[i].GetComponent<ShrinkScript>().play();
				}
				else
				{
					Active_dice[i].GetComponent<Rigidbody>().mass *= 8;
				}
			}
			to_be_rolled = Tens;
			crit_roll = true;
		}
		else
		{
			Dice.clear_dice();
			current_sum = 0;
			critAkku = 0;
			SuccessEffects.reset();
		}
		Active_dice.Clear();
		Tens = -1;
		Roll(to_be_rolled, crit_roll);
		UI.toggle_all(false);

		base_accel = Input.acceleration;

		UI.update_result_display(current_sum, new Color(0.3f, 0.3f, 0.3f, 0.7f), FontStyle.Normal);
	}

	// Update is called once per frame
	void Update () {

		UI = GUI.GetComponentInChildren<UIScript>(false);

		//Counting the result
		if(!Dice.still_rolling() && Tens == -1)	//TODO Also no acceleration?
		{
			current_sum += count();
			//float delay = (Tens > 0) ? 0.4f : 0.2f;
			float delay = (Tens + Active_dice.Count * 0.4f + 1) * 0.05f + 0.1f;		//TODO: Is this delay good? Collect feedback
			this.Invoke("evaluate", delay);
		}

		Vector3 accel_delta = Input.acceleration - base_accel;
		base_accel = Input.acceleration;
		if(accel_delta.magnitude > 0.4f)	//TODO: Whats a good threshold?		//Add shake on Key?
		{
			//Correcting axes:
			if( Screen.orientation == ScreenOrientation.LandscapeLeft)
			{
				accel_delta = new Vector3(- accel_delta.x, accel_delta.y, accel_delta.z);
			}
			GameObject[] dice = GameObject.FindGameObjectsWithTag("Die");
			foreach(GameObject die in dice)
			{
				//Vector3 force = new Vector3(Random.value - 0.5f, 2 * Random.value - 0.5f, Random.value - 0.5f);
				Vector3 force = new Vector3(Random.value - 0.5f, 2 * Random.value - 0.5f, Random.value - 0.5f) - accel_delta * shake_force;
				die.GetComponent<Rigidbody>().AddForce(force);
			}
		}
	}

	private void Roll(int dice_num, bool crit)
	{
		int line_break = Mathf.RoundToInt(Mathf.Sqrt(dice_num));
		for(int i = 0; i < dice_num; ++i)
		{
			Vector3 spawn_offset = new Vector3(i % line_break, i/line_break, Random.value * 2) * distance;

			GameObject die = Dice.roll_die(Spawn_center + distance * spawn_offset, UI.get_crit_list());
			// give it a random rotation
			die.transform.Rotate (new Vector3 (Random.value * 360, Random.value * 360, Random.value * 360));
			die.GetComponent<Rigidbody>().velocity = Throw_force + new Vector3(0, -1, 0) + (new Vector3 (Random.value, Random.value, Random.value) + die.transform.position - Spawn_center);
			die.GetComponent<Rigidbody>().angularVelocity = new Vector3 (Random.value, Random.value, Random.value) * Throw_force.magnitude;
			die.GetComponent<Rigidbody>().mass = die_mass;
			if(Tens != 0)

				Active_dice.Add(die);

			if(crit)
			{
				die.GetComponent<Renderer>().material = crit_material;
			}
		}
	}

	private void evaluate()
	{
		bool crits = (Tens != 0);
		if(crits)
		{
			UI.update_result_display(current_sum);
		}
		else
		{
			UI.update_result_display(current_sum, Color.black, FontStyle.Bold);
			GetComponent<History>().AddRoll(UI.roll_num(), UI.keep_num(), critAkku, current_sum);	//Record Roll
		}
		UI.toggle_all(!crits);		//No Tens means roll is over
		UI.switch_mode_crit(crits);
		UI.set_roll_enabled(true);
	}

	//Calcs Sum and sets Tens
	//SIDEEFFECT: Sets "Tens", darkens dice that are not kept
	private int count()
	{
		int result = 0;
		int crits = 0;
		Active_dice = Active_dice.OrderBy(go=>go.GetComponent<Die>().value).ToList();
		Active_dice.Reverse();

		for(int i = 0; i < Active_dice.Count; ++i)
		{
			if (i <  UI.keep_num())
			{
				int value = Active_dice[i].GetComponent<Die>().value;
				result += value;
				if (UI.is_crit(value))
				{
					crits++;
				}
			}
			else
			{
				Active_dice[i].GetComponent<Darken>().set_brightness(0.3f);	
			}
		}
		Tens = crits;
		critAkku += crits;
		return result;
	}

}