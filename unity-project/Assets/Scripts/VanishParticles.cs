using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishParticles : MonoBehaviour {

	public ParticleSystem PS_prefab;

	private GameObject particles;

	// Use this for initialization
	void Start () {
		particles = GameObject.Instantiate(PS_prefab.gameObject);
		particles.name = this.name + " Particles";
	}
	
	public void play()
	{
		if(true)//if(!particles.GetComponent<ParticleSystem>().isPlaying)
		{
			particles.SetActive(true);
			particles.transform.position = transform.position;
			particles.GetComponent<ParticleSystem>().Play();
		}
	}

	void OnDestroy() 
	{
		if(this.isActiveAndEnabled)
		{
			play();
		}
		GameObject.Destroy(particles, 1.0f);
	}

	void OnDisable() 
	{
		play();
	}
}
