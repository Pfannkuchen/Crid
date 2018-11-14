using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonToNetwork : MonoBehaviour {

	public enum Purpose{
		Server,
		Player
	}

	public NetworkManager networkManager;
	public Purpose purpose;	
	public bool SERVER_IS_PLAYING = false;

	
	// Update is called once per frame
	void OnMouseDown()
	{
		Debug.Log("Selecting network mode: " + purpose);
		networkManager.networkAddress = "192.168.0.11";
		networkManager.networkPort = 7777;

		if (purpose == Purpose.Server){
			if (SERVER_IS_PLAYING){
				networkManager.StartHost();
			} else {
				networkManager.StartServer();
			}
			
			FieldManager.Instance.setServer(true);
		} else {
			networkManager.StartClient();
			FieldManager.Instance.setServer(false);
		}
	}
}
