using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MouseDownSFX : MonoBehaviour, IPointerDownHandler {

	public AudioSource UISpeaker;
	public AudioClip sfx;

	public void OnPointerDown (PointerEventData eventData) 
	{
		UISpeaker.clip = sfx;
		UISpeaker.Play();
	}
}
