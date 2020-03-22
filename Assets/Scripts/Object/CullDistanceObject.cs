using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullDistanceObject : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _cullObjectList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _cullObjectList.Add(transform.Find("Root").gameObject);
        foreach(GameObject obj in _cullObjectList)
        {
            obj.SetActive(false);
        }
    }

    public void CullDistanceActive()
    {
        foreach (GameObject obj in _cullObjectList)
        {
            obj.SetActive(true);
        }
    }

    public void CullDistanceInactive()
    {        
        foreach (GameObject obj in _cullObjectList)
        {
            obj.SetActive(false);
        }
    }

}
