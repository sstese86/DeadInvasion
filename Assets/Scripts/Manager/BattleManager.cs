using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Level;
using NaviEnt;

namespace NaviEnt.Game
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        List<Enemy> Enemies = new List<Enemy>();

        public int EnemyRemains { get => Enemies.Count; }

        void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        public void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            GameEventManager.Instance.OnEenemyRemainsUpdateCallback();
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
            GameEventManager.Instance.OnEenemyRemainsUpdateCallback();
        }

        public void InitBattleGame()
        {
            PoolManager.Instance.InitializePool();
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