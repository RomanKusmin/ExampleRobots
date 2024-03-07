using EvaArchitecture.Controllers._Bases;
using EvaArchitecture.Core;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.EventHelpers;
using Game._Events;
using Game.Controllers.PlayerControllers;
using UnityEngine;

namespace Game.Controllers.BotControllers
{
    public class EnemyAttack : BaseMonoBehaviour
    {
        [SerializeField] private float _distanceCanShoot;
        [SerializeField] private Transform _muzzleTransform;
        
        private int _attackDamage = 2;
        private float _timeBetweenAttacks = 0.5f;
        private EnemyHealth _enemyHealth;
        private float _timeNextShoot;

        private GameObject PlayerGameObject =>
            PlayerController.Instance.IsNull() ? null : PlayerController.Instance.gameObject;

        protected override void Awake()
        {
            base.Awake();
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        protected override void Update()
        {
            base.Update();

            if (Time.time >= _timeNextShoot)
            {
                _timeNextShoot = Time.time + _timeBetweenAttacks;
                if (CheckCanShoot())
                {
                    Shoot();
                }
            }
        }

        private bool CheckCanShoot()
        {
            if (_enemyHealth.CurrentHealth <= 0) 
                return false;
            
            var playerGameObject = PlayerGameObject;
            if (playerGameObject.IsNull()) 
                return false;
            
            var tr = _muzzleTransform.transform;
            var distance = Vector3.Distance(_muzzleTransform.position, playerGameObject.transform.position);
            if (distance > _distanceCanShoot) 
                return false;
            
            var pos = _muzzleTransform.position;
            var forward = _muzzleTransform.forward;

            var ray = new Ray(pos, forward);
            if (!Physics.Raycast(ray, out var hit, distance))
                return false;

            if (hit.IsNull()
                || hit.collider.IsNull()
                || hit.collider.gameObject.IsNull()
                || hit.collider.gameObject != playerGameObject)
                return false;
            
            return true;
        }

        private void Shoot()
        {
            Eva.GetEvent<GameEventPlayerReceivedDamage>().Publish(_attackDamage);
        }
    }
}