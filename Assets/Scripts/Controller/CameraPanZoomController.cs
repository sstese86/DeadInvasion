using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt;
using System;
using NaviEnt.Game;

public class CameraPanZoomController : MonoBehaviour
{
    [SerializeField]
    Camera _camera = null;

    [Header("CameraSetup", order = 0)]
    [SerializeField]
    float _panSpeed = 1f;

    [SerializeField]
    float _cameraHeight = 40f;
    [SerializeField]
    float _cameraMinHeight = 5f;
    [SerializeField]
    float _cameraMaxHeight = 100f;


    [Space]
    [SerializeField]
    float _cameraDistance = 15f;
    [SerializeField]
    float _cameraMinDistance = 0.1f;
    [SerializeField]
    float _cameraMaxDistance = 50f;

    [Header("MapSizeSetup", order = 1)]
    [SerializeField]
    float _mapSizeX = 100f;
    [SerializeField]
    float _mapSizeY = 100f;


    Vector3 lastWorldPanPosition = Vector3.zero;
    Vector3 touchStart = Vector3.zero;
    int panFingerId = 0;
    bool wasZoomingLastFrame = false;
    Vector2[] lastZoomPositions = new Vector2[]{Vector2.zero,Vector2.zero};
    

    private void Start()
    {
        CameraInit();
        GameEventManager.onMissionObjectSelected += MoveToTarget;

    }

    private void OnDestroy()
    {
        GameEventManager.onMissionObjectSelected -= MoveToTarget;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.touchSupported || Application.isMobilePlatform)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }

        CameraZOffset();
        
    }
    void CameraInit()
    {
        Vector3 InitPosition = Vector3.zero;
        InitPosition.y = _cameraHeight;
        InitPosition.z = -_cameraDistance;
        _camera.transform.localPosition = InitPosition;
    }

    void HandleMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lastWorldPanPosition = _camera.ScreenToWorldPoint(GetMousePosition());
        }else if(Input.GetMouseButton(0))
        {
            CameraPan(GetMousePosition());
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        CameraZoom(scroll * 15f);
    }

    void HandleTouch()
    {
        switch(Input.touchCount)
        {
            case 1: //Panning
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    Vector3 lastTouchPos = new Vector3(touch.position.x, touch.position.y, 100f);
                    lastWorldPanPosition = _camera.ScreenToWorldPoint(lastTouchPos);
                    panFingerId = touch.fingerId;
                }else if(touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                {
                    Vector3 newTouchPos = new Vector3(touch.position.x, touch.position.y, 100f);
                    CameraPan(newTouchPos);
                }
                break;
            case 2: // Zooming
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    //Zoom based on the distance between the new positions compared to the distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;
                    CameraZoom(offset * .05f);

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false;
                break;


        }
    }


    void CameraPan(Vector3 newPanPosition)
    {

        Vector3 newWorldPosition = _camera.ScreenToWorldPoint(newPanPosition);
        Vector3 offset = lastWorldPanPosition - newWorldPosition;
        Vector3 move = new Vector3(offset.x * _panSpeed, 0f, offset.z * _panSpeed);
        transform.position += move;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -_mapSizeX / 2f, _mapSizeX / 2f);
        pos.z = Mathf.Clamp(transform.position.z, -_mapSizeY / 2f, _mapSizeY / 2f);

        transform.position = pos;
 
    }



    void CameraZoom(float value)
    {
        Vector3 newPosition = Vector3.zero;
        newPosition = _camera.transform.localPosition;
        _cameraHeight = Mathf.Clamp(newPosition.y - value, _cameraMinHeight, _cameraMaxHeight);
        newPosition.y = _cameraHeight;
        _camera.transform.localPosition = newPosition;
            // _cameraHolder.transform.position = newPosition;
    }


    void CameraZOffset()
    {        
        Vector3 newPosition = Vector3.zero;
        _cameraDistance = Mathf.Lerp(_cameraMinDistance, _cameraMaxDistance, (_cameraHeight / _cameraMaxHeight));

        newPosition = _camera.transform.localPosition;
        newPosition.z = -_cameraDistance;
        //float newDistance = Mathf.Clamp(-newPosition.z + value, _cameraMinDistance, _cameraMaxDistance);
        //newPosition.z = -newDistance;
        _camera.transform.localPosition = newPosition;
    }

    Vector3 GetMousePosition()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 100f;
        return pos;
    }


    void MoveToTarget(Vector3 target)
    {
        gameObject.transform.position = target;
    }
}
