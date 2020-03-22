using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SocketType
{
    Head,
    Body,
    RightHand,
    LeftHand,
    HUD
}

public class SocketIdentifier : MonoBehaviour
{
    public SocketType socketType;
}
