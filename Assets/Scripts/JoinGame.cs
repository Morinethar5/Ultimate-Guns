﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

	List<GameObject> roomList = new List<GameObject>();

	private NetworkManager networkManager;

	[SerializeField]
	private Text status;

	[SerializeField]
	private GameObject roomListItemPrefab;

	[SerializeField]
	private Transform roomListParent;

	void Start() {
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null) {
			networkManager.StartMatchMaker ();
		}

		RefreshRoomList ();
	}

	public void RefreshRoomList(){
		networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
		status.text = "Loading...";
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
		status.text = "";

		if(!success || matchList == null){
			status.text = "Couldn't get room list";
			return;
		}
		ClearRoomList ();
		foreach (MatchInfoSnapshot match in matchList) {
			GameObject _roomListItemGO = Instantiate (roomListItemPrefab);
			_roomListItemGO.transform.SetParent (roomListParent);

			RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
			if (_roomListItem != null) {
				_roomListItem.Setup (match, JoinRoom);
			}

			//Have a compoment sit on the gameobject that will take care of setting up the name/amount of users
			//as well as setting up a callback function that will join the game.

			roomList.Add (_roomListItemGO);
		}
		if (roomList.Count == 0) {
			status.text = "No rooms available.";
		}
	}

	void ClearRoomList(){
		for (int i = 0; i < roomList.Count; i++) {
			Destroy (roomList [i]);
		}

		roomList.Clear ();
	}

	public void JoinRoom(MatchInfoSnapshot _match) {
		networkManager.matchMaker.JoinMatch (_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
		status.text = "Joining game...";
		ClearRoomList ();
	}
}