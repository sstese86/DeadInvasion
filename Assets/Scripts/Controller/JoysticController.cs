using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NaviEnt;

public class JoysticController : MonoBehaviour
{
    Vector2 _virtualJoysticAxis;

    InputManager _inputmanager = null;

    [SerializeField]
    int _movementRange = 100;
    [SerializeField]
    GameObject _joystickBase = null;
    [SerializeField]
    GameObject _joystickTip = null;
    [SerializeField]
    GameObject _joystickTipActive = null;

    bool _enable;
    int _fingerID;
    Touch _touch;
    float _alpha;


    Vector2 _rectSize;
    CanvasGroup _canvasGroup = null;

    Vector3 _startPosition, _joysticHandelPosition;
    Vector2 _touchStartPosition, _touchEndPosition;
    Vector2 _direction;



    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _joystickBase.transform.position;
        _canvasGroup = GetComponent<CanvasGroup>();
        _inputmanager = InputManager.Instance;
        _rectSize = GetComponent<RectTransform>().rect.size;
    }

    void Update()
    {
        if (_enable)
        {
            _canvasGroup.alpha = 1f;
            if (Input.touchCount > 0)
            {

                for (int i = 0; i < Input.touchCount; i++)
                {
                    _touch = Input.GetTouch(i);

                    if (_touch.position.x > _rectSize.x || _touch.position.y > _rectSize.y)
                    {
                        _touch.fingerId = -1;
                        Debug.Log("Touch is out of range:: Posisiont: " + _touch.position.ToString());
                        Debug.Log("FingerId: " + _touch.fingerId);


                    }

                    if (_touch.fingerId == _fingerID)
                    {
                        Debug.Log("Valid Input FingerID: " + _touch.fingerId);
                        Debug.Log("Valid Input Position: " + _touch.position.ToString());
                        if (_touch.phase == TouchPhase.Began)
                        {
                            _touchStartPosition = _touch.position;
                            _joystickTip.transform.position = _touchStartPosition;
                            _joystickBase.transform.position = _touchStartPosition;
                        }
                        else if (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Ended)
                        {
                            _touchEndPosition = _touch.position;
                            float x = _touchEndPosition.x - _touchStartPosition.x;
                            float y = _touchEndPosition.y - _touchStartPosition.y;

                            _direction.Set(x, y);
                            _direction.Normalize();

                            _joysticHandelPosition.Set(
                                _touchStartPosition.x + (_direction.x * Mathf.Clamp(Mathf.Abs(x), 0, _movementRange)),
                                _touchStartPosition.y + (_direction.y * Mathf.Clamp(Mathf.Abs(y), 0, _movementRange)),
                                0);

                            _virtualJoysticAxis.Set(Mathf.Clamp(x / _movementRange, -1, 1), Mathf.Clamp(y / _movementRange, -1, 1));
                            _joystickTip.transform.position = _joysticHandelPosition;
                        }

                    }
                }
            }

        }
        else
        {
            _joystickTip.transform.position = _startPosition;
            _joystickBase.transform.position = _startPosition;
            _virtualJoysticAxis.Set(0f, 0f);


            // Temporay implementation for input suport when environment is not mobile.
            _canvasGroup.alpha = 0f;
            InputSuport();
        }

        if(_inputmanager != null)
            _inputmanager.MovementAxis = _virtualJoysticAxis;
    }

    public void InputSuport()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        _virtualJoysticAxis.Set(x, y);

    }




    public void EnableTouch()
    {
        _enable = true;

        _fingerID = Input.GetTouch(Input.touchCount - 1).fingerId;
        Debug.Log("CallFromJoystick::FingerID: " + _fingerID);
        _joystickTipActive.SetActive(true);
    }

    public void DisableTouch()
    {
        _enable = false;
        _fingerID = -1;
        _joystickTipActive.SetActive(false);
    }

}
