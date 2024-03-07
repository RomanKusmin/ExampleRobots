using System.Collections;
using System.Collections.Generic;
using EvaArchitecture.Controllers._Bases;
using EvaArchitecture.Core;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.EventHelpers;
using Game._Events;
using UnityEngine;

namespace Game.Controllers.PlayerControllers
{
    public class PlayerHealth : SingletonMonoBehaviour<PlayerHealth>
    {
        public override void Subscribe(bool mustSubscribe)
        {
            base.Subscribe(mustSubscribe);
            Eva.GetEvent<GameEventPlayerReceivedDamage>().Subscribe(mustSubscribe, OnEvent);
        }

        #region Serialized fields
        
        [SerializeField] private int _currentHealth;
        [SerializeField] private float _flashSpeed = 5f;
        [SerializeField] private Color _flashColour = new Color(1f, 0f, 0f, 0.1f);
        [SerializeField] private GameObject _atCameraDeath;
        [SerializeField] private AudioSource _audioSourceDamage;
        
        #endregion

        #region private Fields
        
        private int _maxHealth;
        private Animator _anim;
        private PlayerController _playerController;
        private bool _isDead;
        
        #endregion

        #region Properties
        
        private Camera Camera => GameController.Instance.IsNull() ? null : GameController.Instance.Camera;
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        #endregion

        #region unity methods

        protected override void Awake()
        {
            base.Awake();
            _anim = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _currentHealth = _maxHealth;
        }

        protected override void Start()
        {
            base.Start();
            ShowHealth();
        }

        #endregion
        
        #region public methods

        /// <summary>
        /// This method is called from the animator event
        /// </summary>
        public bool IsDead()
        {
            return _isDead;
        }

        /// <summary>
        /// This method is called from the animator event
        /// </summary>
        public void StartSinking()
        {
        }

        #endregion
        
        #region private methods
        
        private void OnEvent(GameEventPlayerReceivedDamage ev, object model, List<object> results)
        {
            if (!(model is int intModel))
                return;
            
            TakeDamage(intModel);
        }
        
        private void TakeDamage(int amount)
        {
            StartCoroutine(ShowDamageImage());
            _currentHealth -= amount;
            ShowHealth();
            _audioSourceDamage.Play();
            if (_currentHealth <= 0 && !_isDead)
            {
                _isDead = true;
                Death();
            }
        }

        private void Death()
        {
            var cameraTransform = Camera.transform;
            cameraTransform.position = _atCameraDeath.transform.position;
            cameraTransform.rotation = _atCameraDeath.transform.rotation;
            _playerController.enabled = false;
            _anim.enabled = true;
            _anim.SetTrigger("Die");
            _audioSourceDamage.Play();
        }

        private IEnumerator ShowDamageImage()
        {
            var ev = Eva.GetEvent<GameEventPlayerDamageImageColorChanged>();
            ev.Publish(_flashColour);
            var currentTime = Time.time;
            var previousTime = currentTime;
            var untilTime = currentTime + _flashSpeed;
            
            while (currentTime < untilTime)
            {
                currentTime = Time.time;
                
                var color = Color.Lerp(_flashColour, Color.clear,
                    _flashSpeed * (currentTime - previousTime));
                ev.Publish(color);
                
                previousTime = currentTime;
                yield return new WaitForSeconds(0.1f);
            }

        }

        private void ShowHealth()
        {
            if (_currentHealth < 0)
                return;
            
            Eva.GetEvent<GameEventPlayerHealthChanged>().Publish(_currentHealth);
        }
        
        #endregion
    }
}
