using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessEffects : MonoBehaviour {

	public AudioClip[] sfx;
	
	private static int succ_count;
	private AudioSource[] audio;
	private bool[] success_list;
	private bool done = false;
	private int start_index = 0;

	// Has to be Awaken, not Start!
	void Awaken () 
	{
		success_list = new bool[1];
	}

	void Start()
	{
		audio = GetComponents<AudioSource>();
		start_index = audio.Length;
		update_audio();
	}

	// Update is called once per frame
	void Update () {
		if(!done)
		{
			check_and_play(GetComponent<Die>().value);
		}
	}

	public void set_sfx(AudioClip[] clips)
	{
		sfx = clips;	//
		update_audio();
	}
	
	public void set_successes(bool[] list)
	{
		success_list = list;
	}
	
	public static void reset()
	{
		succ_count = 0;
		//GetComponent<Light>().enabled = false;
		//GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
	}
	
	private void update_audio()	//Adds AudioScource if necessary and sets clips
	{
		for(int i = 0; i < sfx.Length; ++i)
		{
			AudioSource comp;
			if(i + start_index < audio.Length)
			{
				comp = audio[i + start_index];
			}
			else
			{
				comp = this.gameObject.AddComponent<AudioSource>();
			}
			comp.clip = sfx[i];
		}
		
		audio = GetComponents<AudioSource>();
	}
	
	public void trim_audio()	//Delets unused AudioScources; TODO Combine with update_audio?
	{
		for(int i = audio.Length; i > sfx.Length + start_index; --i)	//TODO Check Edgecases
		{
			Destroy(audio[i]);
		}
		audio = GetComponents<AudioSource>();
	}
	
	public void check_and_play(int num)
	{
		num--;
		if(num >= 0 && num < success_list.Length && success_list[num])
		{
			do_effect();
		}
	}
	
	public void do_effect()		//TODO	Should this add a light component or use an existing one?
	{
		done = true;
		//Audio
		int num = (succ_count >= sfx.Length) ? sfx.Length - 1 : succ_count;	//Clamp
		audio[num + start_index].Play();
		succ_count++;
		
		//Visual
		if(QualitySettings.GetQualityLevel() > 3)	//TODO Is this good?
		{
			GetComponent<Light>().enabled = true;
		}
		GetComponent<Renderer>().material.SetColor("_EmissionColor", GetComponent<Light>().color);
	}
}
