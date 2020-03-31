using UnityEngine;
using System.Collections;

namespace NaviEnt.Game
{
    public class Enemy : MonoBehaviour
    {
        private void OnEnable()
        {
            BattleManager.Instance.AddEnemy(this);
        }

        public void EnemyDeadCallback()
        {
            BattleManager.Instance.RemoveEnemy(this);
        }    
    }
}