using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NaviEnt
{ 
    [System.Serializable]
    public struct AudioClipSetup
    {
        public AudioClip clip;
        public float delay;
        public float volume;
        public float variation;
    }
}

namespace NaviEnt.Game
{
    public class GameSoundClip : MonoBehaviour
    {
        protected int selector = 0;

        protected void PlaySFXSound(List<AudioClipSetup> _listOfClips)
        {
            if (_listOfClips.Count != 0)
            {
                selector = Mathf.FloorToInt(Random.Range(0, _listOfClips.Count));
                AudioManager.Instance.PlaySoundSFX(_listOfClips[selector].clip, _listOfClips[selector].volume, _listOfClips[selector].variation , _listOfClips[selector].delay);
            }
        }

    }
}