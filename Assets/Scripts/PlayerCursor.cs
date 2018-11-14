using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour {
	
	public static List<PlayerCursor> allPlayerCursors = new List<PlayerCursor>();
	public GameObject playerCharacterPrefab;

	public static Vector3[] allPlayerCursorPositions(){
		Vector3[] pos = new Vector3[allPlayerCursors.Count];
		for (int i = 0; i < pos.Length; i++){
			pos[i] = allPlayerCursors[i].transform.position;
		}
		return pos;
	}

	void OnEnable(){
		allPlayerCursors.Add(this);
		
		GetComponentInChildren<MeshRenderer>().material.color = ColorSettings.getPlayerColor(allPlayerCursors.Count - 1);

		Instantiate(playerCharacterPrefab, this.transform.position, Quaternion.identity).GetComponent<PlayerCharacter>().setPlayerCursor(this);
	}

	/* 
	void OnDisable()
	{
		allPlayerCursors.Remove(this);
	}
	*/
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)){
			Debug.Log("Updating player position");
			transform.position = FieldManager.GetTouchPosition();
		}
	}
}
