using System.Collections;
using EvaArchitecture.Controllers._Bases;
using Game.Controllers.PlayerControllers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Controllers.BotControllers
{
    public class BotController : BaseMonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSourceFootSteps;

        private EnemyHealth _enemyHealth;
        private bool _isWalking;

        protected override void Awake()
        {
            base.Awake();
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(BotAI());
        }

        private IEnumerator BotAI()
        {
            while (!_enemyHealth.IsDead())
            {
                var dist = Vector3.Distance(gameObject.transform.position, PlayerController.Instance.transform.position);
                if (dist > _navMeshAgent.stoppingDistance)
                {
                    if (!_isWalking)
                    {
                        _navMeshAgent.enabled = true;
                        if (_navMeshAgent.isStopped)
                            _navMeshAgent.isStopped = false;

                        if (!_animator.enabled)
                            _animator.enabled = true;

                        _isWalking = true;
                        _audioSourceFootSteps.Play();
                    }

                    _navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
                }
                else
                {
                    if (_isWalking)
                    {
                        _navMeshAgent.isStopped = true;
                        _navMeshAgent.enabled = false;
                        _animator.enabled = false;
                        _audioSourceFootSteps.Stop();
                    }

                    _isWalking = false;
                }

                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
