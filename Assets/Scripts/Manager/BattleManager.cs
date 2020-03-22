using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Level;

namespace NaviEnt.Game
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadMainMenu()
        {
            LevelManager.Instance.MoveToMainMenu();
        }
    }
}