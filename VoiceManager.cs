using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VoiceConnection))]
[RequireComponent(typeof(Recorder))]
public class VoiceManager : MonoBehaviourPunCallbacks
{

    private VoiceConnection voiceConnection;
    private Recorder recorder;
    private ConnectAndJoin connectAndJoin;

    public Image micImagePanel;
    public Sprite micOnSprite;
    public Sprite micOffSprite;
    // Start is called before the first frame update
    void Start()
    {
        voiceConnection = GetComponent<VoiceConnection>();
        recorder = GetComponent<Recorder>();
        connectAndJoin = GetComponent<ConnectAndJoin>();
        ConnectVoice();
        SetRecording(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M)) {
            ToggleMic();
        }
    }

    public void SetRecording(bool isRecording){
        recorder.IsRecording = isRecording;
        micImagePanel.sprite = isRecording ? micOnSprite : micOffSprite;
    }

    public void ToggleMic(){
        SetRecording(!recorder.IsRecording);
    }

    public void ConnectVoice()
    {
        string roomName = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Room Name" + roomName);
        if (string.IsNullOrEmpty(roomName))
            {
                this.connectAndJoin.RoomName = string.Empty;
                this.connectAndJoin.RandomRoom = true;
            }
            else
            {
                this.connectAndJoin.RoomName = roomName.Trim();
                this.connectAndJoin.RandomRoom = false;
            }
            if (this.voiceConnection.Client.InRoom)
            {
                this.voiceConnection.Client.OpLeaveRoom(false);
            }
            else if (!this.voiceConnection.Client.IsConnected)
            {
                this.voiceConnection.ConnectUsingSettings();
            }
    }

}
