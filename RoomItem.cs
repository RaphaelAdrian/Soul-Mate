using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviourPunCallbacks
{
    public TMP_Text roomNameUIText;
    public TMP_Text roomSizeUIText;

    string roomName;
    public void UpdateRoom(string roomName, int numOfPlayers, int roomSize)
    {
        this.roomNameUIText.text = roomName;
        this.roomSizeUIText.text = numOfPlayers + " / " + roomSize;

        this.roomName = roomName;
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(roomName);
    }
}
