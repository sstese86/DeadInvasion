using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Data;

namespace NaviEnt.Game
{
    public abstract class Item<T> : Item where T : Item<T>
    {
                       
    }

    public abstract class Item : MonoBehaviour
    {
        public int Amount { get; set; }
        public ItemData ItemData { get; set; }
        // Start is called before the first frame update

        public virtual void Start()
        {
        }

    }
}