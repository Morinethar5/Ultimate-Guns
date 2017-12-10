using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class RoomListItem : MonoBehaviour {

	private MatchInfoSnapshot match;

	public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
	private JoinRoomDelegate joinRoomCallback;

	[SerializeField]
	private Text roomNameText;

	public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallBack){
		match = _match;
		joinRoomCallback = _joinRoomCallBack;

		roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
	}

	public void JoinRoom() {
		joinRoomCallback.Invoke (match);
	}
}
