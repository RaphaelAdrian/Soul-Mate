using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class NetworkedRigidbody : MonoBehaviourPunCallbacks
{
    void OnCollisionEnter2D(Collision2D other) {
        PhotonView otherPV = other.gameObject.GetComponent<PhotonView>();
        if (otherPV) {
            if (otherPV.Owner != photonView.Owner)
                photonView.TransferOwnership(otherPV.Owner);
        }
    }
}
