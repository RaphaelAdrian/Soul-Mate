using System.Collections;
using System.Collections.Generic;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.UI;

public class VoiceLevelMeter : MonoBehaviour
{
    public Color activeColor = Color.green;
    private Color initColor;
    public Recorder recorder;
    
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        initColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        float level = recorder.LevelMeter.CurrentPeakAmp;
        this.image.color = level > 0.25f ? activeColor : initColor;
    }
}
