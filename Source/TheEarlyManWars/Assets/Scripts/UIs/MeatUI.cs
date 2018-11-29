using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeatUI : MonoBehaviour
{
	MeatSystem _meatSystem;
	[SerializeField]
	Text _meatText;

	void Start ()
	{
		_meatSystem = FindObjectOfType<MeatSystem>();
	}

	void Update ()
	{
		_meatText.text = _meatSystem.meat.ToString();
	}
}
