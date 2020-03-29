using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class HUDEntitySelector : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameEventManager.onSelectedEntityChangedCallback += UpdateTarget;
        }
        private void OnDestroy()
        {
            GameEventManager.onSelectedEntityChangedCallback -= UpdateTarget;
        }

        private void UpdateTarget(IEntity obj, Transform trans)
        {
            transform.position = trans.position;
            transform.parent = trans;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}