using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public RoomItem roomItemPrefab;
    public GameObject roomItemParent;
    public GameObject noRoomsText;
    public int roomSize = 20;
    private Dictionary<string, RoomInfo> cachedRoomList;
    List<RoomItem> roomItems;


    void Start() {
        if (!PhotonNetwork.InLobby) {
            StartCoroutine(CheckJoinLobby());
        }
        
        roomItems = new List<RoomItem>();
        cachedRoomList = new Dictionary<string, RoomInfo>();
    }

    private IEnumerator CheckJoinLobby()
    {
        // sometimes player fails to join the lobby 
        // so we have to check every 1seconds until joined
        PhotonNetwork.JoinLobby();
        yield return new WaitForSeconds(0.5f);
        if (!PhotonNetwork.InLobby) {
            StartCoroutine(CheckJoinLobby());
        }
    }

    private void UpdateRoomStatus()
    {
        if (roomItems.Count == 0) {
            noRoomsText.SetActive(true);
        } else {
            noRoomsText.SetActive(false);
        }
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("Room");
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    public void CreateRoom() {
        int randRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions(){IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        PhotonNetwork.CreateRoom("Room " + randRoomNumber, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Failed to Create Room. Trying again");
        CreateRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        // clear all existing rooms
        ClearRooms();
        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }


    private void ClearRooms()
    {
        foreach(RoomItem room in roomItems) {
            Destroy(room.gameObject);
        }
        roomItems.Clear();
    }

     private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // Remove room from cached room list if it got closed, became invisible or was marked as removed
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }

                continue;
            }

            // Update cached room info
            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            // Add new room info to cache
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }

    private void UpdateRoomListView()
    {
        foreach (RoomInfo room in cachedRoomList.Values)
        {
            if (room.PlayerCount < room.MaxPlayers) {
                RoomItem roomItem = Instantiate(roomItemPrefab, roomItemParent.transform);
                roomItem.UpdateRoom(room.Name, room.PlayerCount, room.MaxPlayers);
                roomItems.Add(roomItem);
            }
            
        }

        UpdateRoomStatus();
    }
    

}
