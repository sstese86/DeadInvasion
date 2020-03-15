using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Game;

public class HUDHealthBar : MonoBehaviour
{
    [SerializeField]
    Transform _slider = null;
    // Start is called before the first frame update
    public void UpdateHealthSlider(float value)
    {
        Vector3 scale = _slider.localScale;
        scale.x = value;
        _slider.localScale = scale;
    }
}
