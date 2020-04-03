using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace NaviEnt.Game
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        string _key = string.Empty;

        [SerializeField]
        GameObject _player = null;
        [SerializeField]
        GameObject _camSetup = null;

        CinemachineVirtualCamera _viurtualCam = null;
        // Start is called before the first frame update
        void Start()
        {
            SpawnPlayer();
        }
        
        void SpawnCamera(Transform followTarget, Transform lookAtTarget)
        {
            if(_camSetup)
            {
                GameObject cam = Instantiate(_camSetup);
                _viurtualCam = cam.GetComponentInChildren<CinemachineVirtualCamera>();
                _viurtualCam.LookAt = lookAtTarget;
                _viurtualCam.Follow = followTarget;
            }
        }

        void SpawnPlayer()
        {
            if (_player != null)
            {
                GameObject player = Instantiate(_player);
                PlayerController controller = player.GetComponent<PlayerController>();
                if(controller)
                {
                    controller.IsInputEnable = false;
                    player.transform.position = transform.position;
                    controller.Root.rotation = transform.rotation;
                    player.transform.parent = null;
                    SpawnCamera(controller.GetCamTargetFollow, controller.GetCamTargetLookAt);
                    controller.SpawnCallback();
                }
            }
            gameObject.SetActive(false);
        }

    }
}