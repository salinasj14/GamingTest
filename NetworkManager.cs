using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {
	const string VERSION = "v0.1";
	public string roomName = "";
	private int maxPlayer = 1;
	int maxChatMessages = 5;
	List<string> chatMessages;
	private Room[] game;
	bool connecting = false;
	private string maxPlayerString = "4";
	private Vector3 up;
	private Vector2 scrollPosition;

	// Use this for initialization
	void Start()
	{
		//PhotonNetwork.ConnectUsingSettings(VERSION);
		PhotonNetwork.player.NickName = PlayerPrefs.GetString("Username", "   ");
		chatMessages = new List<string>();
	}
	void OnDestroy(){
		PlayerPrefs.SetString("Username", "   ");
	}

	public void AddChatMessage(string m){
		GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllBuffered, m);
	}

	[PunRPC]
	void AddChatMessage_RPC(string m){
		while(chatMessages.Count >= maxChatMessages){
			chatMessages.RemoveAt(0);
		}
		chatMessages.Add(m);
	}

	void OnGUI(){
		GUI.color = Color.grey;
		float w = 0.3f; // proportional width (0..1)
		if(connecting == false ) {
			GUILayout.BeginArea( new Rect(0 , 0, Screen.width, Screen.height) );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUI.color = new Color(128f,0f,128f,.8f);
			GUILayout.Label("Username: ");
			GUI.color = new Color(255f,255f,0f,.8f);
			PhotonNetwork.player.NickName = GUILayout.TextField(PhotonNetwork.player.NickName);
			GUILayout.EndHorizontal();

			if( GUILayout.Button("Lets Play!") ) {
				connecting = true;
				Connect ();

			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
		//DestroyObject(
		if(PhotonNetwork.connected == true ) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			foreach(string msg in chatMessages) {
				GUILayout.Label(msg);
			}

			GUILayout.EndVertical();
			GUILayout.EndArea();

		}

		if (PhotonNetwork.insideLobby == true) {
			GUI.Box(new Rect((Screen.width*(1-w))/2.8f, 0, 400, 550),"");
			GUI.color = Color.white;
			GUILayout.BeginArea (new Rect((Screen.width*(1-w))/2.8f , 0 , 400, 550));
			GUI.color = Color.cyan;
			GUILayout.Box ("Lobby");
			GUI.color = Color.white;
			var roomOptions = new RoomOptions();
			GUILayout.Label("Room Name:");
			roomName = GUILayout.TextField(roomName);
			roomOptions.CleanupCacheOnLeave = true;
			roomOptions.IsOpen = true;
			roomOptions.IsVisible = true;
			roomOptions.MaxPlayers = 4;
			GUILayout.Label ("Max amount of players 1 - 4:");
			maxPlayerString = GUILayout.TextField (maxPlayerString,2);
			if (maxPlayerString != "") {

				maxPlayer = int.Parse (maxPlayerString);

				if (maxPlayer > 4) maxPlayer = 4;
				if (maxPlayer == 0) maxPlayer = 1;
			}
			else
			{
				maxPlayer = 1;
			}
			
			if ( GUILayout.Button ("Create Room ") ) {
				if (roomName != "" && maxPlayer > 0) {
					roomOptions.MaxPlayers = (byte)maxPlayer;
					PhotonNetwork.CreateRoom(roomName, roomOptions,null);
				}
			}

			GUILayout.Space (20);
			GUI.color = Color.yellow;
			GUILayout.Box ("Rooms Open");
			GUI.color = Color.blue;
			GUILayout.Space (20);

			scrollPosition = GUILayout.BeginScrollView(scrollPosition, false,true,GUILayout.Width(400), GUILayout.Height(300));


			foreach (RoomInfo game in PhotonNetwork.GetRoomList ())
			{
				GUI.color = Color.green;
				GUILayout.Box (game.Name + " " + game.PlayerCount + "/" + game.MaxPlayers);
				if ( GUILayout.Button ("Join Room") ) {
					PhotonNetwork.JoinRoom(game.Name);
				}
			}
			GUILayout.EndScrollView ();
			GUILayout.EndArea ();
		}
	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings(VERSION);
	}

	void OnJoinedLobby(){
		//RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 4 };
		//PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
		Debug.Log("OnJoinedLobby");
	}
	
	void OnJoinedRoom(){
		PhotonNetwork.LoadLevel("pres");
	}
	
	void OnPhotonRandomJoinFailed(){
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null );
	}
}