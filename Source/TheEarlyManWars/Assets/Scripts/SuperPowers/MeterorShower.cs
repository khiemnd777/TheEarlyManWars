using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class MeterorShower : MonoBehaviour
{
	public int cooldown = 10;
	public float shakeDuration = .2f;
	public float shakeMagnitude = .8f;
	[SerializeField]
	MonsterDisplayList _monsterList;
	[SerializeField]
	CameraShake _cameraShake;
	bool _inCooldownProgress;
	Settings _settings;
	Button _button;
	float _cooldownCounter;

	void Awake ()
	{
		_button = FindObjectOfType<Button> ();
	}

	void Start ()
	{
		_settings = FindObjectOfType<Settings> ();
		_button.onClick.AddListener (() => OnClick ());
	}

	void Update ()
	{
		CalculateCooldownProgress();
	}

	void OnClick ()
	{
		if (_inCooldownProgress) return;
		Execute ();
		_inCooldownProgress = true;
	}

	void Execute ()
	{
		StartCoroutine(_cameraShake.Shake(shakeDuration / _settings.deltaSpeed, shakeMagnitude));
		foreach (var monster in _monsterList.list)
		{
			var damage = monster.hp / 2;
			monster.TakeDamage (damage);
		}
	}

	void CalculateCooldownProgress ()
	{
		if (_settings.deltaSpeed <= 0) return;
		if (!_inCooldownProgress) return;
		_button.interactable = false;
		if (_cooldownCounter < 1f)
		{
			_cooldownCounter += Time.deltaTime / cooldown * _settings.deltaSpeed;
			return;
		}
		_inCooldownProgress = false;
		_button.interactable = true;
		_cooldownCounter = 0f;
	}
}
