using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class ItemSoundClip : GameSoundClip
    {
        [SerializeField]
        List<AudioClipSetup> _use = new List<AudioClipSetup>();

        [SerializeField]
        List<AudioClipSetup> _hitActor = new List<AudioClipSetup>();

        [SerializeField]
        List<AudioClipSetup> _attack = new List<AudioClipSetup>();

        [SerializeField]
        List<AudioClipSetup> _reload = new List<AudioClipSetup>();

        public void PlaySoundItemUse()
        {
            PlaySFXSound(_use);
        }

        public void PlaySoundItemHitActor()
        {
            PlaySFXSound(_hitActor);
        }

        public void PlaySoundItemAttack()
        {
            PlaySFXSound(_attack);
        }

        public void PlaySoundItemReload()
        {
            PlaySFXSound(_reload);
        }
    }
}