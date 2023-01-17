using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class RoomSize : MonoBehaviourPunCallbacks
{
    int roomMaxSize;
    // Start is called before the first frame update
    void Start()
    {
        roomMaxSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        UpdateRoom();
    }

    
    private void UpdateRoom()
    {
        int numOfPlayersInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        GetComponent<TMP_Text>().text = numOfPlayersInRoom + " / " + roomMaxSize;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        UpdateRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        UpdateRoom();
    }
}
