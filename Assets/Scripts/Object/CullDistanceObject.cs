using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullDistanceObject : MonoBehaviour
{
    GameObject _root = null;
    // Start is called before the first frame update
    void Start()
    {
        _root = transform.Find("Root").gameObject;
        _root.SetActive(false);
    }

    public void CullDistanceActive()
    {
        _root.SetActive(true);
    }

    public void CullDistanceInactive()
    {
        _root.SetActive(false);
    }

}
