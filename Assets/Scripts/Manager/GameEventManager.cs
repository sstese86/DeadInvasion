using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class GameEventManager : MonoBehaviour
{
    static GameEventManager _instance;
    public static GameEventManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }
    public static event Action<Entity> onSelectedEntiyChanged = delegate { };
    public static event Action<Vector3> onMissionObjectSelected = delegate { };

    public void OnMissionObjectSelected(Vector3 position)
    {
        onMissionObjectSelected.Invoke(position);
    }

}
