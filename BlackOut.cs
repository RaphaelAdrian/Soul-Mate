using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOut : MonoBehaviour
{
    public PlayerType playerType;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.BlackOutPlayer(playerType, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
