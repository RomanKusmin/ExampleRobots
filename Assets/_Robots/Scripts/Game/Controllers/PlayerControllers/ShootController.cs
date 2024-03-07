using System.Collections;
using Core.Helpers;
using EvaArchitecture.Controllers._Bases;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.Logger;
using Game.Controllers.BotControllers;
using Game.Controllers.UiControllers.UiWindowControllers;
using Services.InputServices;
using UnityEngine;

namespace Game.Controllers.PlayerControllers
{
	public class ShootController : BaseMonoBehaviour
	{
		private const string TINT_COLOR_NAME = "_TintColor";
		private const float EFFECTS_DISPLAY_TIME = 0.2f;
		private const string SHOOT_LAYER_MASK = "Shootable";
		
		#region Serializable fields
		
		[SerializeField] private Animator _animator;
		[SerializeField] private LineRenderer _BigCanon01L;
		[SerializeField] private LineRenderer _BigCanon01R;
		[SerializeField] private LineRenderer _BigCanon02L;
		[SerializeField] private LineRenderer _BigCanon02R;
		[SerializeField] private LineRenderer _SmallCanon01L;
		[SerializeField] private LineRenderer _SmallCanon01R;
		[SerializeField] private LineRenderer _SmallCanon02L;
		[SerializeField] private LineRenderer _SmallCanon02R;
		[SerializeField] private AudioSource _audioSourceFire;
		[SerializeField] private AudioClip _audioBigCanon;
		[SerializeField] private AudioClip _audioSmallCanon;
		[SerializeField] private int _damagePerShot = 20;
		[SerializeField] private float _timeBetweenBullets = 0.15f;
		[SerializeField] private float _range = 1024f;
		
		#endregion

		#region private Fields
		
		private float _timer;
		private RaycastHit _shootHit;
		private int _shootLayerMask;
		private LineRenderer _gunLineL;
		private LineRenderer _gunLineR;

		#endregion
		
		#region Properties
		
		private Camera Camera => GameController.Instance.IsNull() ? null : GameController.Instance.Camera;
		
		#endregion
		
		#region Unity events
		
		protected override void Awake()
		{
			base.Awake();
			_shootLayerMask = LayerMask.GetMask(SHOOT_LAYER_MASK);
			_gunLineL = _BigCanon01L;
			_gunLineR = _BigCanon01R;
		}

		protected override void Update()
		{
			base.Update();
			_timer += Time.deltaTime;

			if (PlayerController.Instance.UseTouchPad)
			{
				if (PlayerExplorerUiWindowController.Instance.FireTouchPad.Touched && _timer >= _timeBetweenBullets)
				{
					Shoot();
				}
			}
			else
			{
				if (InputService.Instance.GetButton("Fire1") && _timer >= _timeBetweenBullets)
				{
					Shoot();
				}
			}
			
			if (_timer >= _timeBetweenBullets * EFFECTS_DISPLAY_TIME)
			{
				DisableEffects();
			}
		}
		
		#endregion
		
		#region private methods

		private void DisableEffects()
		{
			_gunLineL.enabled = false;
			_gunLineR.enabled = false;
		}

		private void Shoot()
		{
			_timer = 0f;
			if (Camera.IsNull())
				return;

			PlayAudioFire();

			_gunLineL.enabled = true;
			_gunLineR.enabled = true;

			_animator.SetBool("ShootBigCanon_A", true);
			
			Vector3 gunPosition;
			var ray = Camera.ScreenPointToRay(ScreenHelper.GetScreenCenter());
			if (Physics.Raycast(ray, out _shootHit, _range))
			{
				var enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();
				if (enemyHealth != null)
				{
					Log.Info(() => $"Physics.Raycast, this.y={this.transform.parent.position.y}, target={_shootHit.collider.gameObject.name}");
					enemyHealth.TakeDamage(_damagePerShot, _shootHit.point);
				}

				var dist = Vector3.Distance(_gunLineR.gameObject.transform.position, _shootHit.point);
				gunPosition = new Vector3(0f, 0f, dist);
			}
			else
			{
				if (gameObject.transform.parent.gameObject == PlayerController.Instance.gameObject)
				{
					gunPosition = new Vector3(0f, 0f, _range);
				}
				else
				{
					gunPosition = new Vector3(0f, 0f, 15f);
				}
			}
			_gunLineL.SetPosition(1, gunPosition);
			_gunLineR.SetPosition(1, gunPosition);
		}
		
		#endregion
		
		#region private methods

		private void ShootBigCanonA() => ShootCanon(_audioBigCanon, _BigCanon01L, _BigCanon01R);
		private void ShootBigCanonB() => ShootCanon(_audioBigCanon, _BigCanon02L, _BigCanon02R);
		private void ShootSmallCanonA() => ShootCanon(_audioSmallCanon, _SmallCanon01L, _SmallCanon01R);
		private void ShootSmallCanonB() => ShootCanon(_audioSmallCanon, _SmallCanon02L, _SmallCanon02R);

		private void ShootCanon(
			AudioClip audioClip, 
			Renderer leftRenderer, 
			Renderer rightRenderer)
		{
			PlayAudioFire(audioClip);

			var colorName = TINT_COLOR_NAME;
			var c = leftRenderer.material.GetColor(colorName);
			c.a = 1f;
			leftRenderer.material.SetColor(colorName, c);
			rightRenderer.material.SetColor(colorName, c);
			
			StartCoroutine(FadOutShootCoroutine(leftRenderer, rightRenderer));
		}

		private IEnumerator FadOutShootCoroutine(
			Renderer leftRenderer, 
			Renderer rightRenderer)
		{
			var colorName = TINT_COLOR_NAME;
			var c = leftRenderer.material.GetColor(colorName);
			while (c.a > 0)
			{
				c.a -= 0.1f;
				leftRenderer.material.SetColor(colorName, c);
				rightRenderer.material.SetColor(colorName, c);
				yield return null;
			}
		}

		private void PlayAudioFire(AudioClip audioClip = null)
		{
			if (!_audioSourceFire.enabled)
				return;
			
			if (audioClip != null)
				_audioSourceFire.clip = audioClip;
			
			if (_audioSourceFire.clip == null)
				return;
			
			_audioSourceFire.Play();
		}

		#endregion
	}
}
