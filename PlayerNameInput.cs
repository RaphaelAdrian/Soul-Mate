using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField nameInputTextfield;
    public MenuController menuController;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Next();
        }
    }

    public void Next(){
        string playerName = nameInputTextfield.text;
        if (!string.IsNullOrWhiteSpace(playerName)) {
            menuController.playerName = Truncate(playerName, 10);
            menuController.QuickStart();
        }
    }

    public void Back(){
        gameObject.SetActive(false);
    }

    public string Truncate(string value, int maxLength) {
        if (!string.IsNullOrEmpty(value) && value.Length > maxLength){
            return value.Substring(0, maxLength);
        }

        return value;
    }
    
}
