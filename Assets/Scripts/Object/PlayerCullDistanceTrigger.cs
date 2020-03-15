using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt;

public class PlayerCullDistanceTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CullDistanceObject>())
        {
            other.GetComponent<CullDistanceObject>().CullDistanceActive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit " + other.name);
        if (other.GetComponent<CullDistanceObject>())
        {
            other.GetComponent<CullDistanceObject>().CullDistanceInactive();
        }
    }
}
