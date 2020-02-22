using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt
{
    public class InputManager : MonoBehaviour
    {
        static InputManager _instance;
        public static InputManager Instance => _instance;

        float _zoomInput = 0.0f;

        public float ZoomInput => _zoomInput;
        public float HorizontalAxis { get; set; }
        public float VertivalAxis { get; set; }


        // Update is called once per frame
        private void Awake()
        {
            if (_instance != null) Destroy(gameObject);
            else _instance = this;
        }

        void Update()
        {
            _zoomInput = TouchInputPinch();
            CheckAndroidBackButton();
        }

        float TouchInputPinch()
        {
            float value = 0.0f;

            if(Input.touchCount >= 2)
            { 
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
                float difference = currentMagnitude - prevMagnitude;

                value = difference * 0.1f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                value = Input.GetAxis("Mouse ScrollWheel") * 10f;
            }
            return value;
        }

        void CheckAndroidBackButton()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
            }
        }


    }
}