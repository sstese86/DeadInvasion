using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoreMountains.Feedbacks;

namespace NaviEnt
{

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        // Start is called before the first frame update
        [SerializeField]
        MMFeedbacks SFXFeedback = null;
        [SerializeField]
        MMFeedbacks UIFeedback = null;
        [SerializeField]
        MMFeedbacks AmbientFeedback = null;
        [SerializeField]
        AudioSource _musicAudioSource = null;

        [Space]
        [SerializeField]
        AudioClip _startMusic = null;


        MMFeedbackSound _sfxSoundFeedback = null;
        MMFeedbackSound _uiSoundFeedback = null;
        MMFeedbackSound _ambientFeedback = null;


        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        public void PlaySoundSFX(AudioClip clip)
        {

            _sfxSoundFeedback.MinPitch = 0.95f;
            _sfxSoundFeedback.MaxPitch = 1.05f;
            _sfxSoundFeedback.MinVolume = 0.95f;
            _sfxSoundFeedback.MaxVolume = 1.05f;

            _sfxSoundFeedback.Sfx = clip;
            _sfxSoundFeedback.Play(transform.position, 0);

        }

        public void PlaySoundSFX(AudioClip clip, float volume = 1f)
        {

            _sfxSoundFeedback.MinPitch = 1f - 0.05f;
            _sfxSoundFeedback.MaxPitch = 1f + 0.05f;
            _sfxSoundFeedback.MinVolume = volume - 0.05f;
            _sfxSoundFeedback.MaxVolume = volume + 0.05f;

            _sfxSoundFeedback.Sfx = clip;
            _sfxSoundFeedback.Play(transform.position, 0);

        }

        public void PlaySoundSFX(AudioClip clip, float volume = 1f, float variation = 0.1f , float delay = 0f)
        {
            if(delay != 0f)
            {
                StartCoroutine(PlaySoundWithDelay(clip, volume, variation, delay));
            }
            else
            {
                _sfxSoundFeedback.MinPitch = 1f - variation;
                _sfxSoundFeedback.MaxPitch = 1f + variation;
                _sfxSoundFeedback.MinVolume = volume - 0.05f;
                _sfxSoundFeedback.MaxVolume = volume + 0.05f;

                _sfxSoundFeedback.Sfx = clip;

                _sfxSoundFeedback.Play(transform.position, 0);
            }
        }



        public void PlaySoundUI(AudioClip clip)
        {
            _uiSoundFeedback.Sfx = clip;
            _uiSoundFeedback.Play(transform.position, 0);
        }

        public void PlaySoundAmbient(AudioClip clip)
        {
            _ambientFeedback.Sfx = clip;
            _ambientFeedback.Play(transform.position, 0);
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicAudioSource.clip = clip;
            _musicAudioSource.Play();
        }

        public void PlayMusic()
        {
            _musicAudioSource.clip = _startMusic;
            _musicAudioSource.Play();
        }
        void Start()
        {
            _sfxSoundFeedback = SFXFeedback.Feedbacks[0] as MMFeedbackSound;
            _uiSoundFeedback = UIFeedback.Feedbacks[0] as MMFeedbackSound;
            _ambientFeedback = AmbientFeedback.Feedbacks[0] as MMFeedbackSound;
        }

        IEnumerator PlaySoundWithDelay(AudioClip clip, float volume = 1f, float variation = 0.1f, float delay = 0f)
        {
            yield return new WaitForSeconds(delay);
            _sfxSoundFeedback.MinPitch = 1f - variation;
            _sfxSoundFeedback.MaxPitch = 1f + variation;
            _sfxSoundFeedback.MinVolume = volume - 0.05f;
            _sfxSoundFeedback.MaxVolume = volume + 0.05f;

            _sfxSoundFeedback.Sfx = clip;

            _sfxSoundFeedback.Play(transform.position, 0);
            yield return null;
        }

    }
}