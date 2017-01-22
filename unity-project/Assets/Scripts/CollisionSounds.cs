using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CollisionSounds : MonoBehaviour {

	public float variance_factor;
	public float max_impact;

	public AudioClip self_collision_sfx;	//Collision with the same Material
	public AudioClip other_sfx;

	private AudioSource sound;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision collision) 
	{
		PhysicMaterial mat = collision.gameObject.GetComponent<MeshCollider>().material;
		//float hardness = Mathf.Abs(collision.relativeVelocity.y);
		float hardness = collision.relativeVelocity.magnitude;
		play_sfx(mat, hardness);
	}

	public void play_sfx(PhysicMaterial mat, float hardness)
	{
		sound.volume = Mathf.Clamp(hardness, 0, max_impact) / max_impact;
			
		if (mat == GetComponent<MeshCollider>().material)
		{
			sound.pitch = 1 + variance_factor * (Random.value * 4 - 2);
			sound.clip = self_collision_sfx;
			sound.Play();
		}
		else
		{
			sound.pitch = 1 + variance_factor * (Random.value - 0.5f);
			sound.clip = other_sfx;
			sound.Play();
		}
	}

}
