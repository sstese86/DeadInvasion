using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGroup : MonoBehaviour
{
    static ManagerGroup _instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }
}
