using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviourPunCallbacks
{
    [SerializeField] string goToSceneName = "MainMenu";    
    public void ExitToScene()
    {
        StartCoroutine(ExitScene());
    }

    public void ExitLobby()
    {
        StartCoroutine(ExitToLobby());
    }

    
    IEnumerator ExitScene()
    {
        PhotonNetwork.LeaveRoom(false);
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(goToSceneName);
    }

     IEnumerator ExitToLobby()
    {
        PhotonNetwork.LeaveLobby();
        while (PhotonNetwork.InLobby)
            yield return null;
        SceneManager.LoadScene(goToSceneName);
    }

}
