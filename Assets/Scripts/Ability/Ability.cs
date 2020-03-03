using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaviEnt.Game
{
    public abstract class Ability<T> :Ability where T : Ability<T>
    {
        public virtual void UseAbility()
        {

        }
    }
    

    public abstract class Ability : MonoBehaviour
    {

    }
}