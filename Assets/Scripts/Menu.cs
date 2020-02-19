using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NaviEnt.UI
{
    public abstract class Menu<T>: MonoBehaviour where T: Menu<T>
    {
        static T _instance;
        public static T Instance => _instance;
        protected virtual void Awake()
        {
            if (_instance != null) Destroy(gameObject);
            else _instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }
        public static void MenuOpen()
        {
            Instance.gameObject.SetActive(true);
        }

        public static void OnBackPressed()
        {
            Instance.gameObject.SetActive(false);
        }
    }

    //public abstract class Menu : MonoBehaviour
    //{

    //}
}