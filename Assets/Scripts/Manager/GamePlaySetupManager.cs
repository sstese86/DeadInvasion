using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Level;

namespace NaviEnt
{
    public class GamePlaySetupManager : MonoBehaviour
    {
        public TextMesh textMesh;
        // Start is called before the first frame update
        void Start()
        {
            textMesh.text = LevelManager.missionId.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadMainMenu()
        {
            LevelManager.Instance.LoadMainMenu();
        }
    }
}