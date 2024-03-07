using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechShoot : MonoBehaviour 
{
	public LineRenderer BigCanon01L;
	public LineRenderer BigCanon01R;
	public LineRenderer BigCanon02L;
	public LineRenderer BigCanon02R;

	public LineRenderer SmallCanon01L;
	public LineRenderer SmallCanon01R;
	public LineRenderer SmallCanon02L;
	public LineRenderer SmallCanon02R;

	public AudioClip audioBigCanon;
	public AudioClip audioSmallCanon;

	private Animator _animator;
	private Transform _body;
	private int _direction = 1;
	private float _counter = 0;
	private float _rot;
	private float _shooterCounter = 0f;
	private AudioSource _audioSource;

	// Use this for initialization
	private void Start() 
	{
		_animator = GetComponent<Animator> ();
		_body =  transform.Find ("Mech/Root/Pelvis/Body");
		_audioSource = GetComponent<AudioSource> ();
	}
	
	private void LateUpdate() 
	{
		_counter += Time.deltaTime;
		if (_counter > 3)
		{
			_direction *= -1;
			_counter = 0;
		}
		
		_rot += Time.deltaTime *20f  * _direction;

		_body.localRotation = Quaternion.Euler (new Vector3 (_rot, 180f, 0f));

		_shooterCounter += Time.deltaTime;

		if (_shooterCounter > 7) 
		{
			_animator.SetBool ("ShootSmallCanon", true);
			StartCoroutine (FadOutBigCanon01());
			StartCoroutine (FadOutBigCanon02());
		}
	}

	private void ShootBigCanonA() 
	{
		_audioSource.clip = audioBigCanon;
		_audioSource.Play ();

		var c = BigCanon01L.material.GetColor ("_TintColor");
		c.a = 1f;
		BigCanon01L.material.SetColor("_TintColor",  c);
		BigCanon01R.material.SetColor("_TintColor",  c);
		StartCoroutine (FadOutBigCanon01());
	}

	private IEnumerator FadOutBigCanon01() 
	{
		var c = BigCanon01L.material.GetColor ("_TintColor");
		while (c.a > 0) 
		{
			c.a -= 0.1f;
			BigCanon01L.material.SetColor("_TintColor",  c);
			BigCanon01R.material.SetColor("_TintColor",  c);
			yield return null;
		}
	}

	private void ShootBigCanonB() 
	{
		_audioSource.clip = audioBigCanon;
		_audioSource.Play ();

		var c = BigCanon01L.material.GetColor ("_TintColor");
		c.a = 1f;
		BigCanon02L.material.SetColor("_TintColor",  c);
		BigCanon02R.material.SetColor("_TintColor",  c);
		StartCoroutine (FadOutBigCanon02());
	}

	private IEnumerator FadOutBigCanon02() 
	{
		var c = BigCanon02L.material.GetColor ("_TintColor");
		while (c.a > 0) 
		{
			c.a -= 0.1f;
			BigCanon02L.material.SetColor("_TintColor", c);
			BigCanon02R.material.SetColor("_TintColor", c);
			yield return null;
		}
	}
	
	private void ShootSmallCanonA() 
	{

		_audioSource.clip = audioSmallCanon;
		_audioSource.Play ();

		var c = SmallCanon01L.material.GetColor ("_TintColor");
		c.a = 1f;
		
		SmallCanon01L.material.SetColor("_TintColor", c);
		SmallCanon01R.material.SetColor("_TintColor", c);
		StartCoroutine (FadOutSmallCanon01());
	}

	private IEnumerator FadOutSmallCanon01() 
	{
		var c = SmallCanon01L.material.GetColor ("_TintColor");
		while (c.a > 0) 
		{
			c.a -= 0.1f;
			SmallCanon01L.material.SetColor("_TintColor", c);
			SmallCanon01R.material.SetColor("_TintColor", c);
			yield return null;
		}
	}

	private void ShootSmallCanonB() 
	{
		_audioSource.clip = audioSmallCanon;
		_audioSource.Play();

		var c = SmallCanon01L.material.GetColor ("_TintColor");
		c.a = 1f;
		
		SmallCanon02L.material.SetColor("_TintColor", c);
		SmallCanon02R.material.SetColor("_TintColor", c);
		StartCoroutine (FadOutSmallCanon02());
	}

	private IEnumerator FadOutSmallCanon02() 
	{
		var c = SmallCanon02L.material.GetColor ("_TintColor");
		while (c.a > 0) {
			c.a -= 0.1f;
			SmallCanon02L.material.SetColor("_TintColor", c);
			SmallCanon02R.material.SetColor("_TintColor", c);
			yield return null;
		}
	}
}
