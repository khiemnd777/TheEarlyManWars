using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class MeterorShower : MonoBehaviour
{
	public int damage = 20;
	public int cooldown = 100;
	public float shakeDuration = .2f;
	public float shakeMagnitude = .8f;
	[SerializeField]
	MonsterDisplayList _monsterList;
	[SerializeField]
	CameraShake _cameraShake;
	bool _inCooldownProgress = true;
	Settings _settings;
	TechnologyManager _technologyManager;
	Button _button;
	float _cooldownCounter;

	void Start ()
	{
		_button = GetComponent<Button> ();
		_settings = FindObjectOfType<Settings> ();
		_technologyManager = FindObjectOfType<TechnologyManager>();
	}

	void Update ()
	{
		CalculateCooldownProgress ();
	}

	public void OnClick ()
	{
		if (_inCooldownProgress) return;
		Execute ();
		_inCooldownProgress = true;
	}

	void Execute ()
	{
		StartCoroutine (_cameraShake.Shake (shakeDuration / _settings.deltaSpeed, shakeMagnitude));
		var list = _monsterList.list.ToArray();
		foreach (var monster in list)
		{
			if (monster == null || monster is Object && monster.Equals (null)) continue;
			var operatedDamage = Mathf.FloorToInt(damage * (1 + _technologyManager.superPowerDamageRate));
			monster.TakeDamage (operatedDamage);
		}
	}

	void CalculateCooldownProgress ()
	{
		if (_settings.deltaSpeed <= 0) return;
		if (!_inCooldownProgress) return;
		_button.interactable = false;
		if (_cooldownCounter <= 1f)
		{
			var operatedCooldown = cooldown * (1 - _technologyManager.superPowerCooldownRate);
			_cooldownCounter += Time.deltaTime / operatedCooldown * _settings.deltaSpeed;
			return;
		}
		_inCooldownProgress = false;
		_button.interactable = true;
		_cooldownCounter = 0f;
	}
}
