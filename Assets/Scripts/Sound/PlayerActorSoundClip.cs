using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class PlayerActorSoundClip : GameSoundClip
    {
        [SerializeField]
        List<AudioClipSetup> _hit = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _dead = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _attack = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _jump = new List<AudioClipSetup>();

        public void PlaySoundHit()
        {
            PlaySFXSound(_hit);  
        }

        public void PlaySoundDead()
        {
            PlaySFXSound(_dead);                
        }

        public void PlaySoundAttack()
        {
            PlaySFXSound(_attack);
        }
        public void PlaySoundJump()
        {
            PlaySFXSound(_jump);               
        }
    }
}