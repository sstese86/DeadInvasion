using System.Collections;
using System.Collections.Generic;
using System;


public enum GraphicQuality
{
    High,
    Normal,
    Low
}


[Serializable]
public struct SystemData
{

    public bool isFirstTime;
    
    //"PlayerID Should be Hash Value. Might need it for Cloudsave"
    public string playerId;

    public float masterVolume;
    public float sfxVolume;
    public float musicVolume;
    public GraphicQuality graphicQuality;

}
