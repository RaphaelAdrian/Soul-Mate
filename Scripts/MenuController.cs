using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviourPunCallbacks
{
    public GameObject hideWhileLoadingPanel;
    public GameObject nameInputPanel;
    public GameObject connectingText;
    public GameObject logo;

    [HideInInspector]
    public string playerName;

    public void Start() {
        connectingText.SetActive(true);
        hideWhileLoadingPanel.SetActive(false);
    }
    public override void OnConnectedToMaster() {
        PhotonNetwork.AutomaticallySyncScene = true;
        StartCoroutine(AnimateUIAfterLoad());
    }

    private IEnumerator AnimateUIAfterLoad()
    {
        yield return new WaitForSeconds(0.8f);
        logo.GetComponent<Animator>().SetBool("hasLoaded", true);
        connectingText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        hideWhileLoadingPanel.SetActive(true);
    }

    // Start is called before the first frame update
    public void QuickStart() {
        // if name doesn't exist, then ask player
        if (string.IsNullOrEmpty(playerName)) {
            nameInputPanel.SetActive(true);
            return;
        }
        PhotonNetwork.LocalPlayer.NickName = playerName;
        hideWhileLoadingPanel.SetActive(false);
        SceneManager.LoadScene("Lobby");
    }
    public void Exit() {
        Application.Quit();
    }
    
}
