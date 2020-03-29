using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Level;

namespace NaviEnt.Game
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        List<Enemy> Enemies = new List<Enemy>();
        
        void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        public void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
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