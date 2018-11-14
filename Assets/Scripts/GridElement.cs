using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

	float score;
	MeshRenderer rend;

	void Start()
	{
		rend = GetComponent<MeshRenderer>();
	}

	public void update(Vector3[] pos){
		for (int i = 0; i < pos.Length; i++){
			float dist = Vector3.Distance(pos[i], this.transform.position);
			if (dist < 10f){
				score += (10f - dist * dist) * Time.deltaTime * 3f;
			}
		}

		score -= Time.deltaTime * 3f;

		score = Mathf.Clamp(score, 0.1f, 5f);

		//this.transform.localScale = new Vector3(0.9f, score, 0.9f);
		this.transform.localScale = new Vector3(0.9f, 0.1f, 0.9f);
		rend.material.color = Color.Lerp(Color.gray, Color.magenta, score / 5f);
	}
}
