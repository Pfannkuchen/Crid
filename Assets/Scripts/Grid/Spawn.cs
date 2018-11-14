using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Block {

	MeshRenderer meshRenderer;
	Color colorCache = Color.white;

	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player"){
			return;
		}

		colorCache = meshRenderer.material.color;
		meshRenderer.material.color = Color.green;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag != "Player"){
			return;
		}
		
		meshRenderer.material.color = colorCache;
	}
}
