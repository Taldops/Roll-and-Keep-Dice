/*
 * https://docs.unity3d.com/ScriptReference/Material.SetTextureOffset.html
 */
using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {

	public Vector2 scroll_vector;

	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}

	void Update() {
		Vector2 offset = Time.time * scroll_vector;
		rend.material.SetTextureOffset("_MainTex", offset);
	}
}