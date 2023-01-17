using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class LobbyRoomDoor : MonoBehaviourPunCallbacks
{
    public PlayerType doorColor = PlayerType.Green;
    public RoomManager roomManager;
    public GameObject enabledIndicator;
    GameObject objectThatOccupies;
    bool isOccupied;

    void Start() {
        enabledIndicator.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();

        if (player.gameObject == roomManager.player && !isOccupied){
            photonView.RPC("PunSendFinish", RpcTarget.AllBuffered, (byte)doorColor, true);
            Hashtable hash = new Hashtable (){{"color",(byte)doorColor}};
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player.gameObject == roomManager.player && isOccupied){
            photonView.RPC("PunSendFinish", RpcTarget.AllBuffered, (byte)doorColor, false);
        }
    }
    [PunRPC]
    public void PunSendFinish(byte bytePlayerType, bool isEnter)
    {
        PlayerType playerType = (PlayerType)bytePlayerType;
        roomManager.UpdateStatus(playerType, isEnter);
        enabledIndicator.SetActive(isEnter);
        isOccupied = isEnter;

        if (isEnter)
            GetComponent<AudioComponent>()?.PlayOnNetwork(0);
    }
}
