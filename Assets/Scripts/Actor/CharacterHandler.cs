using UnityEngine;
using System.Collections;
using NaviEnt.Game;

namespace NaviEnt
{
    public class CharacterHandler : Actor, IDamageable, IEntity
    {
        [SerializeField]
        FeedbackHandler _feedbackHandler = null;

        ActorSoundClip _actorSoundClip = null;

        [SerializeField]
        CharacterState _state = new CharacterState();

        HUDHealthBar _healthbar = null;
        public CharacterState State { get => _state; }

        public bool isDead { get; private set; }

        public string EntityName { get; set; }
        public string EntityInfo { get; set; }


        private void OnEnable()
        {
            CurrentHealth = _state.maxHealth;
            _healthbar = GetComponentInChildren<HUDHealthBar>();
            _healthbar?.gameObject.SetActive(true);
        }
        // Use this for initialization
        void Start()
        {
            _actorSoundClip = GetComponent<ActorSoundClip>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool TakeDamage(Team team, int amount)
        {
            if(ActorTeam != team)
            {
                _actorSoundClip?.PlaySoundHit();
                CurrentHealth -= amount;
                IsDead();   
                UpdateCurrentHealthInfo();

                _feedbackHandler?.HitFeedback();
                return true;
            }
            return false;
        }
        public void Heal(int amount)
        {
            CurrentHealth += amount;
            UpdateCurrentHealthInfo();
        }

        bool IsDead()
        {
            if (CurrentHealth < 1)
            {
                isDead = true;
                _actorSoundClip?.PlaySoundDead();
                GetComponent<Collider>().enabled = false;
                _healthbar?.gameObject.SetActive(false);
                return true;
            }
            return false;
        }
        void UpdateCurrentHealthInfo()
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _state.maxHealth);
            float value = (float)CurrentHealth / (float)_state.maxHealth;
            _healthbar.UpdateHealthSlider(value);
        }

        public Team GetTeam()
        {
            return ActorTeam;
        }

        public void UpdateEntityInfo()
        {
            EntityName = ActorName;
            EntityInfo = _state.maxHealth.ToString() + " / " + CurrentHealth.ToString();
            GameEventManager.Instance.OnSelectedEntityChangedCallback(GetComponent<IEntity>());
        }
    }
}