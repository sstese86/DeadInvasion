using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    static UICamera _instance;
    public UICamera Instance => _instance;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;        
    }

}
