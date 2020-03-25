using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaviEnt.Game
{
    public class AIController : MonoBehaviour
    {
    
        CharacterHandler _characterHandler = null;
        bool _isDead = false;
        // Start is called before the first frame update
        void Start()
        {
            _characterHandler = GetComponent<CharacterHandler>();
        }

        // Update is called once per frame
        void Update()
        {
            isDead();
        }


        void isDead()
        {
            if(_isDead)
            {
                gameObject.SetActive(false);
            }

            if (_characterHandler.isDead)
            {
                _isDead = true;
            }
        }
    }
}