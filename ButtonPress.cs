using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class ButtonPress : Trigger
{
    public ButtonType buttonType = ButtonType.All;
    GameObject objectInTrigger;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        OnButtonPressed(other);
    }

    public virtual void OnButtonPressed(Collider2D other)
    {
        // we need to make the button be pressed one at a time
        // to avoid a bug (sometimes, the button doens't return to normal
        // after being pressed by two rigidbodies)
        if (isActivated)
            return;
        if (objectInTrigger != null)
            return;
        
        
        Player player = other.GetComponent<Player>();
        // if button type is all, then everyone can trigger (as long it has rigidbody)
        if (buttonType == ButtonType.All && other.GetComponent<Rigidbody2D>()) {
            // need to check if the player is you to avoid duplicate calls
            if (!PhotonNetwork.OfflineMode && other.GetComponent<PhotonView>().Owner != PhotonNetwork.LocalPlayer) {
                return;
            }
            ActivateButton(true);
            objectInTrigger = other.gameObject;
            
        } else {
            if (player?.playerType.ToString() == buttonType.ToString()) {
                ActivateButton(true);
                objectInTrigger = player.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isActivated)
            return;
        if (objectInTrigger != other.gameObject)
            return;

        Player player = other.GetComponent<Player>();

        // if button type is all, then everyone can trigger (as long it has rigidbody)
        if (buttonType == ButtonType.All && other.GetComponent<Rigidbody2D>()) {
                   // need to check if the player is you to avoid duplicate calls
            if (!PhotonNetwork.OfflineMode && other.GetComponent<PhotonView>().Owner != PhotonNetwork.LocalPlayer) {
                return;
            }
            ActivateButton(false);
        } else {
            if (player?.playerType.ToString() == buttonType.ToString()) {
                ActivateButton(false);
            }
        }
        objectInTrigger = null;
    }
}
public enum ButtonType {
    Green,
    Pink,
    All
}