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

        private void OnDisable()
        {
            BattleManager.Instance.RemoveEnemy(this);
        }
    }
}