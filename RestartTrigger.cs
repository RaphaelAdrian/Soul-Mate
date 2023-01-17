using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RestartTrigger : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player) {
           player.OnDie();
        }
    }
}
