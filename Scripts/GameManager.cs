using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static GameManager instance = null;

    public GameObject spawnPointBlue;
    public GameObject spawnPointPink;
    public GameObject blackOutGrid;
    public bool disableBlackoutGrid;
    internal Player player;
    bool isOfflineMode;

    internal LevelManager levelManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
            
        HandleOfflineMode();
    }

    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        if (!isOfflineMode) {
            if ((byte)PhotonNetwork.LocalPlayer.CustomProperties["color"] == (byte)PlayerType.Green) 
                PlayAsGreen();
            else 
                PlayAsPink();
        } else {
            PlayAsPink();
            PlayAsGreen();
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            levelManager.GoToNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            levelManager.RestartLevel();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Exit>().ExitToScene();
        }
        
    }
    public void PlayAsPink()
    {
        GameObject playerObject = PhotonNetwork.Instantiate("Player_Pink", spawnPointPink.transform.position, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
        if (!disableBlackoutGrid) blackOutGrid.SetActive(true);
    }

    public void PlayAsGreen()
    {
        GameObject playerObject = PhotonNetwork.Instantiate("Player_Green", spawnPointBlue.transform.position, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
        if (!disableBlackoutGrid) blackOutGrid.SetActive(true);
    }

    public void BlackOutPlayer(PlayerType playerType, BlackOut blackOut){
        if (playerType == player.playerType) {
            blackOut.gameObject.SetActive(false);
        }
        blackOut.GetComponent<TilemapRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    }
    
    private void HandleOfflineMode()
    {
        isOfflineMode = PhotonNetwork.PhotonServerSettings.StartInOfflineMode;
        PhotonNetwork.OfflineMode = isOfflineMode;
        if (isOfflineMode && !PhotonNetwork.InRoom)
            PhotonNetwork.CreateRoom("");
        else {
            if (!PhotonNetwork.IsConnectedAndReady)
               SceneManager.LoadScene(0);
        }
    }
}
