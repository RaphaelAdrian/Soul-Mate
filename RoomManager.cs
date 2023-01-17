using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject playerNamesParent;
    public TMP_Text roomNameText;
    public GameObject startButton;
    public GameObject waitingToStartButton;

    public TransitionCanvas transitionCanvas;
    internal GameObject player;
    internal bool isGreenOccupied;
    internal bool isPinkOccupied;



    // Start is called before the first frame update
    void Start()
    {
        transitionCanvas = Instantiate(transitionCanvas);
        transitionCanvas.TransitionIn();

        GameObject playerRow = PhotonNetwork.Instantiate("NameRow", Vector3.zero, Quaternion.identity);
        player = PhotonNetwork.Instantiate("PlayerInRoom", Vector3.zero, Quaternion.identity);

        NameRow nameRow = playerRow.GetComponent<NameRow>();

        // set player parent to the scroll view
        photonView.RPC("PunSetPlayer", RpcTarget.AllBufferedViaServer, nameRow.photonView.ViewID);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }


    [PunRPC]
    private void PunSetPlayer(int photonViewID){
        GameObject nameObject = PhotonNetwork.GetPhotonView(photonViewID).gameObject;
        nameObject.transform.SetParent(playerNamesParent.transform);
        nameObject.transform.localScale = Vector3.one;
    }


    public void UpdateStatus(PlayerType playerType, bool isEnter)
    {
        if (playerType == PlayerType.Green)
            isGreenOccupied = isEnter;
        else
            isPinkOccupied = isEnter;

        // if both is finished, then go to next level
        if (isGreenOccupied && isPinkOccupied) {
            StartCoroutine(NextScene());
        }
    }

    private IEnumerator NextScene()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        transitionCanvas.TransitionOut();
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsMasterClient) {
            int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            buildIndex = buildIndex < SceneManager.sceneCountInBuildSettings ? buildIndex : 0;
            PhotonNetwork.LoadLevel(buildIndex);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
}
