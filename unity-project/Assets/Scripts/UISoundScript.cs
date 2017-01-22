using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UISoundScript : MonoBehaviour {

	//public AudioClip[] result_sfx;
	public AudioClip result_sfx;
	public AudioClip critical_sfx;
	public AudioClip click_sfx;
	public AudioClip start_sfx;

	private AudioSource click_source;
	private AudioSource misc_source;
	private AudioSource result_source;

	// Use this for initialization
	void Start () {
		AudioSource[] sources = GetComponents<AudioSource>();
		misc_source = sources[0];
		click_source = sources[1];
		result_source = sources[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void play_start()
	{
		click_source.clip = start_sfx;
		click_source.Play();
	}

	public void play_click()
	{
		click_source.clip = click_sfx;
		click_source.Play();
	}

	public void play_critical()
	{
		result_source.clip = critical_sfx;
		result_source.Play();
	}

	public void play_result(int level)
	{
		/*
		int num = level;
		if(num >= result_sfx.Length)
		{
			num = result_sfx.Length - 1;
		}
		click_source.clip = result_sfx[num];
		click_source.Play();
		*/
		result_source.clip = result_sfx;
		result_source.Play();
	}

	public void play_any(AudioClip clip)
	{
		misc_source.clip = clip;
		misc_source.Play();
	}
}
