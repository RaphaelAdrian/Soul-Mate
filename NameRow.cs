using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class NameRow : MonoBehaviourPunCallbacks
{
    void Start() {
        GetComponentInChildren<TMP_Text>().text = photonView.Owner.NickName;
    }
}
