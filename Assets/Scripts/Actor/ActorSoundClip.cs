using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    [System.Serializable]
    public struct AudioClipSetup
    {
        public AudioClip clip;
        public float volume;
        public float variation;
    }

    public class ActorSoundClip : MonoBehaviour
    {

        [SerializeField]
        List<AudioClipSetup> _hit = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _dead = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _attack = new List<AudioClipSetup>();

        [SerializeField]
        List<AudioClipSetup> _jump = new List<AudioClipSetup>();

        int selector = 0;

        public void PlaySoundHit()
        {
            if (_hit.Count != 0)
            {
                selector = Mathf.FloorToInt(Random.Range(0, _hit.Count));
                AudioManager.Instance.PlaySoundSFX(_hit[selector].clip, _hit[selector].volume, _hit[selector].variation);
            }       
        }

        public void PlaySoundDead()
        {
            if (_dead.Count != 0)
            {
                selector = Mathf.FloorToInt(Random.Range(0, _dead.Count));
                AudioManager.Instance.PlaySoundSFX(_dead[selector].clip, _dead[selector].volume,_dead[selector].variation);
            }
                
        }

        public void PlaySoundAttack()
        {
            if (_attack.Count != 0)
            {
                selector = Mathf.FloorToInt(Random.Range(0, _attack.Count));
                AudioManager.Instance.PlaySoundSFX(_attack[selector].clip, _attack[selector].volume, _attack[selector].variation);
            }
                
        }
        public void PlaySoundJump()
        {
            if (_jump.Count != 0)
            {
                selector = Mathf.FloorToInt(Random.Range(0, _jump.Count));
                AudioManager.Instance.PlaySoundSFX(_jump[selector].clip, _jump[selector].volume, _jump[selector].variation);
            }
               
        }
    }
}