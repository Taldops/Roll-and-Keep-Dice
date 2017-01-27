using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	[SerializeField] UISoundScript sound;
	[SerializeField] GameObject roll_control;
	[SerializeField] GameObject keep_control;
	[SerializeField] Button roll_button;
	[SerializeField] Button reset_button;
	[SerializeField] GameObject crit_toggles;
	[SerializeField] Text result_display;

	private Toggle[] toggles;

	// Use this for initialization
	void Start () 
	{
		toggles = crit_toggles.GetComponentsInChildren<Toggle>();
	}

	// Update is called once per frame
	void Update () {
		keep_control.GetComponentInChildren<Slider>().maxValue = roll_control.GetComponentInChildren<Slider>().value;
		roll_control.GetComponentInChildren<Text>().text = roll_control.GetComponentInChildren<Slider>().value.ToString();
		keep_control.GetComponentInChildren<Text>().text = keep_control.GetComponentInChildren<Slider>().value.ToString();
	}

	public int roll_num()
	{
		return (int) roll_control.GetComponentInChildren<Slider>().value;
	}

	public int keep_num()
	{
		return (int) keep_control.GetComponentInChildren<Slider>().value;
	}

	public bool is_crit(int roll)
	{
		for(int i = 0; i < toggles.Length; ++i)
		{
			if(toggles[i].isOn && int.Parse(toggles[i].GetComponentInChildren<Text>().text) == roll)
			{
				return true;
			}
		}
		return false;
	}

	public bool[] get_crit_list()
	{
		bool[] list = new bool[10];
		list[7] = toggles[0].isOn;
		list[8] = toggles[1].isOn;
		list[9] = toggles[2].isOn;
		return list;
	}

	public void update_result_display(int result)		//call when a roll is initiated
	{
		result_display.text = "Result: " + result.ToString();
	}

	public void update_result_display(int result, Color color, FontStyle font)		//call when a roll is initiated
	{
		result_display.text = "Result: " + result.ToString();
		result_display.color = color;
		if(font == FontStyle.Bold)
		{
			sound.play_result(2);
			result_display.GetComponent<ShrinkScript>().play();
		}
		result_display.fontStyle = font;
	}
	
	public void switch_mode_crit(bool crit)	//false -> normal roll
	{
		Text b_text = roll_button.GetComponentInChildren<Text>();
		if(crit)
		{
			b_text.text = "Roll Critical!";
			b_text.color = new Color(0.8f,0,0);
			b_text.fontStyle = FontStyle.Bold;
			roll_button.GetComponent<MouseDownSFX>().sfx = sound.click_sfx;
			sound.play_critical();
		}
		else
		{
			b_text.text = "Roll";
			b_text.color = new Color(0,0,0);
			b_text.fontStyle = FontStyle.Normal;
			roll_button.GetComponent<MouseDownSFX>().sfx = sound.start_sfx;
		}
	}
	
	public void set_roll_enabled(bool enabled)	//
	{
		roll_button.interactable = enabled;
		if(roll_button.GetComponentInChildren<Text>().color == new Color(0.8f,0,0))
		{
			roll_button.GetComponent<ShrinkScript>().play();
		}
		//TODO This can be more robust. It depends on the order of enable and switch to critical and the color
	}

	public void toggle_all(bool enabled)
	{
		roll_control.GetComponentInChildren<Slider>().interactable = enabled;
		keep_control.GetComponentInChildren<Slider>().interactable = enabled;
		for(int i = 0; i < toggles.Length; ++i)
		{
			toggles[i].interactable = enabled;
		}

		roll_button.interactable = enabled;
	}

}
