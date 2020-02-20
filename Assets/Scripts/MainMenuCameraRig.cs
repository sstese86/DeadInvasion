using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuCameraRig : MonoBehaviour
{
    [SerializeField]
    RectTransform _scrollSource = null;
    [SerializeField]
    Transform _cameraHolder = null;

    [SerializeField]
    float _cameraHeight = 90f;
    
    [SerializeField]
    float _cameraHorizontalScroll = 10f;
    [SerializeField]
    float _cameraVerticalScroll = 10f;

    bool isBusy = false;

    Vector3 _currentPosition = new Vector3();

    private void Start()
    {
        OnScreenPositionUpdate(Vector2.zero);
    }

    public void OnScreenPositionUpdate(Vector2 pos)
    {
        if (isBusy) return;

        float scrollX = -((_scrollSource.transform.localPosition.x + 1280f)/1280f);
        float scrollY = -((_scrollSource.transform.localPosition.y - 720f) / 720f);

        _currentPosition.Set(scrollX * _cameraHorizontalScroll, 0, scrollY * _cameraVerticalScroll);
        transform.position = _currentPosition;
        _currentPosition.Set(0f, _cameraHeight, 0f);
        _cameraHolder.localPosition = _currentPosition;
    }

    public void TestCall()
    {
        MoveToPosition(Vector3.zero);
    }

    public void MoveToPosition(Vector3 targetPos)
    {
        StartCoroutine(MoveToPositionRoutine(transform.position, targetPos));
    }

    IEnumerator MoveToPositionRoutine(Vector3 startPos, Vector3 targetPos)
    {        
        isBusy = true;
        float time = 0.0f;
        while(time < 1f)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time);
            time += 0.05f;
            yield return new WaitForSeconds(.01f);
        }
        isBusy = false;
    }

}
