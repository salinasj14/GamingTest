//Author: Jose Salinas (networkManager.cs)
//Editor: Joshua Bevell
//
//Spawner.cs controls the spawning of players in a map.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public string playerPrefabName = "CharacterRobotBoy";
    public Transform spawnPoint;
	 public GameObject SpawnSpot;
	 
	private static bool spawn = false;	//Keeps track of whether a playr has been spawned

	void Update(){
		if(spawn == true)
		{
			spawn = false;
			PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.position,spawnPoint.rotation,0);
		}
	}
	
	void Start() {
		PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.position,spawnPoint.rotation,0);
		spawn = false;
		Debug.Log("OnJoinedRoom");
		Debug.Log(PhotonNetwork.countOfPlayers);
		Debug.Log ("My name is " + PhotonNetwork.player.NickName + " with ID = " + PhotonNetwork.player.ID);
	}

}