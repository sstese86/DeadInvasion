using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NaviEnt.Game;

namespace NaviEnt.UI
{
    public abstract class Menu<T>: Menu where T: Menu<T>
    {
        
        public static T Instance { get; private set; }
        public GameObject MenuPanel;
       
        protected virtual void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
        public virtual void MenuOpen()
        {
            Instance.MenuPanel.SetActive(true);
            GameManager.Instance.totalMenuOpened++;
            GameEventManager.Instance.OnUIStateUpdate();
        }

        public void MenuClose()
        {
            Instance.MenuPanel.SetActive(false);
            GameManager.Instance.totalMenuOpened--;
            GameEventManager.Instance.OnUIStateUpdate();
        }



    }

    public abstract class Menu : MonoBehaviour
    {

    }
}