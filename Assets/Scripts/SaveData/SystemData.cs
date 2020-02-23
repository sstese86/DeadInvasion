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
public class SystemData : SaveData<SystemData>
{
    readonly string defaultPlayerId = "PlayerID Should be Hash Value. Might need it for Cloudsave";

    public bool isFirstTime;
    public string playerId;

    public float masterVolume;
    public float sfxVolume;
    public float musicVolume;
    public GraphicQuality graphicQuality;

    public SystemData()
    {
        isFirstTime = true;
        playerId = defaultPlayerId;

        masterVolume = 1f;
        sfxVolume = 0.75f;
        musicVolume = 0.55f;
        graphicQuality = GraphicQuality.Normal;
    }

}
