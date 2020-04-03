using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Game;

public class HUDHealthBar : MonoBehaviour
{
    [SerializeField]
    Transform _slider = null;

    float _value = 1f;

    private void Start()
    {

    }

    public void InitHealthBar()
    {
        _value = 1f;
        gameObject.SetActive(false);
        Vector3 scale = _slider.localScale;
        scale.x = _value;
        _slider.localScale = scale;
    }

    public void UpdateHealthSlider(float value)
    {
       
        _value = value;

        if (_value == 1f) 
        {
            gameObject.SetActive(false); 
        }
        else
        {
            gameObject.SetActive(true);
            Vector3 scale = _slider.localScale;
            scale.x = _value;
            _slider.localScale = scale;

            StopAllCoroutines();
            StartCoroutine(SetHideHealthBarTimer());
        }

    }

    IEnumerator SetHideHealthBarTimer()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }



}


