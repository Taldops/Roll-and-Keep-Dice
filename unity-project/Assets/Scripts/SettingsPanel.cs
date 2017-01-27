using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour {

	public Dropdown qualityDD;
	public Toggle muteToggle;
	public Toggle FPSToggle;
	//public AudioListener Listener;

	// Use this for initialization
	void Start () {
		qualityDD.ClearOptions();
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
		foreach(string setting in QualitySettings.names)
		{
			options.Add(new Dropdown.OptionData(setting));
		}
		qualityDD.AddOptions(options);
		qualityDD.value = QualitySettings.GetQualityLevel();
	}

	public void UpdateQualitySelection()
	{
		QualitySettings.SetQualityLevel(qualityDD.value, true);
	}

	public void ToggleMute()
	{
		AudioListener.pause = muteToggle.isOn;
	}

	public void ToggleFPS()
	{
		GameObject.Find("Canvas").GetComponent<FPSDisplay>().enabled = FPSToggle.isOn;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
