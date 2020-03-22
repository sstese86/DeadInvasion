using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class ActorSocketFinder : MonoBehaviour
    {
        Transform _socketRightHand = null;
        Transform _socketLeftHand = null;
        Transform _socketBody = null;
        Transform _socketHead = null;
        Transform _socketHUD = null;

        public Transform RightHand { get => _socketRightHand; }
        public Transform LeftHand { get => _socketLeftHand; }
        public Transform Body { get => _socketBody; }
        public Transform Head { get => _socketHead; }
        public Transform HUD { get => _socketHUD; }

        private void OnEnable()
        {
            FindSockts();
        }

        void FindSockts()
        {
            SocketIdentifier[] socktes = GetComponentsInChildren<SocketIdentifier>();
            for (int i = 0; i < socktes.Length; i++)
            {
                if (socktes[i].socketType == SocketType.Head)
                    _socketHead = socktes[i].transform;
                if (socktes[i].socketType == SocketType.Body)
                    _socketBody = socktes[i].transform;
                if (socktes[i].socketType == SocketType.RightHand)
                    _socketRightHand = socktes[i].transform;
                if (socktes[i].socketType == SocketType.LeftHand)
                    _socketLeftHand = socktes[i].transform;
                if (socktes[i].socketType == SocketType.HUD)
                    _socketHUD = socktes[i].transform;
            }
        }
    }
}