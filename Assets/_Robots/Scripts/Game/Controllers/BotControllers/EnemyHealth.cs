using EvaArchitecture.Controllers._Bases;
using EvaArchitecture.Core;
using EvaArchitecture.EventHelpers;
using Game._Events;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Controllers.BotControllers
{
    public class EnemyHealth : BaseMonoBehaviour
    {
        private const string DIE_ANIMATOR_PARAMETER = "Die";
        
        [SerializeField] private AudioSource _audioSourceDamage;
        [SerializeField] private int _startingHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private float _sinkSpeed;

        private int _scoreValueDeath = 5000;
        private int _scoreValueDamage = 250;
        private Animator _anim;
        private CapsuleCollider _capsuleCollider;
        private NavMeshAgent _navMeshAgent;
        private bool _isDead;
        private bool _isSinking;

        public int CurrentHealth => _currentHealth;

        public bool IsDead()
        {
            return _isDead;
        }

        protected override void Awake()
        {
            base.Awake();
            _anim = GetComponent<Animator>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _currentHealth = _startingHealth;
        }

        protected override void Update()
        {
            base.Update();
            if (_isSinking)
            {
                transform.Translate(-Vector3.up * _sinkSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// This method is called from the animator event
        /// </summary>
        public void StartSinking()
        {
            _navMeshAgent.enabled = false;
            _isSinking = true;
            GameController.Instance.ScoreAdd(_scoreValueDeath);
            Destroy(gameObject, 2f);
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (_isDead)
            {
                return;
            }

            _audioSourceDamage.Play();
            _currentHealth -= amount;
            GameController.Instance.ScoreAdd(_scoreValueDamage);
            if (_currentHealth <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            Eva.GetEvent<GameEventBattleEnemyDeath>().Publish(null);
            _isDead = true;
            _capsuleCollider.isTrigger = true;
            _anim.SetTrigger(DIE_ANIMATOR_PARAMETER);
            _audioSourceDamage.Play();
        }
    }
}
