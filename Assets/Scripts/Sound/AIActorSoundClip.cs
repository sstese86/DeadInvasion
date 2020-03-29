using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class AIActorSoundClip : GameSoundClip
    {
        [SerializeField]
        List<AudioClipSetup> _idle = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _aggro = new List<AudioClipSetup>();

        [SerializeField]
        List<AudioClipSetup> _hit = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _dead = new List<AudioClipSetup>();

        [SerializeField]
        List<AudioClipSetup> _attack1 = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _attack2 = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _attack1Hit = new List<AudioClipSetup>();
        [SerializeField]
        List<AudioClipSetup> _attack2Hit = new List<AudioClipSetup>();



        public List<AudioClipSetup> GetAttack1HitAudioClips { get => _attack1Hit; }
        public List<AudioClipSetup> GetAttack2HitAudioClips { get => _attack2Hit; }

        public void PlaySoundIdle()
        {
            PlaySFXSound(_idle);
        }
        public void PlaySoundAggro()
        {
            PlaySFXSound(_aggro);
        }
        public void PlaySoundHit()
        {
            PlaySFXSound(_hit);
        }
        public void PlaySoundDead()
        {
            PlaySFXSound(_dead);
        }
        public void PlaySoundAttack1()
        {
            PlaySFXSound(_attack1);
        }
        public void PlaySoundAttack2()
        {
            PlaySFXSound(_attack2);
        }
    }
}