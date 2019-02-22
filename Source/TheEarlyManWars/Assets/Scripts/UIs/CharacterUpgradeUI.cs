using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUpgradeUI : MonoBehaviour
{
	public CharacterUpgradeManager manager;
	public Text nameText;
	public SpriteRenderer display;
	public Text attackText;
	public Text hpText;
	public Button buyExpButton;
	public Button buyDiamondButton;

	Text _buyExpText;
	Text _buyDiamondText;
	string _nameFormat = "{0} lvl. {1}";
	string _attackFormat = "Attack: {0}";
	string _hpFormat = "HP: {0}";
	string _buyExpFormat = "Upgrade ({0}XP)";
	string _buyDiamondFormat = "Upgrade ({0}D)";

	void Awake ()
	{
		buyExpButton.onClick.AddListener (() =>
		{
			manager.UpgradeByExp (() =>
			{
				SetValues ();
			});
		});

		buyDiamondButton.onClick.AddListener (() =>
		{
			manager.UpgradeByDiamond (() =>
			{
				SetValues ();
			});
		});
		_buyExpText = buyExpButton.GetComponentInChildren<Text> ();
		_buyDiamondText = buyDiamondButton.GetComponentInChildren<Text> ();
	}

	void Start ()
	{
		SetValues ();
	}

	void SetValues ()
	{
		nameText.text = string.Format (_nameFormat, manager.characterName, manager.PreviewLevel ());
		attackText.text = string.Format (_attackFormat, manager.PreviewAttackPower ().ToRoundToInt ());
		hpText.text = string.Format (_hpFormat, manager.PreviewHp ().ToRoundToInt ());
		_buyExpText.text = string.Format (_buyExpFormat, manager.upgradedCost.ExperencePoint.ToRoundToInt ());
		_buyDiamondText.text = string.Format (_buyDiamondFormat, manager.upgradedCost.Diamond.ToRoundToInt ());
		// Assign Character Display
		display.sprite = manager.display.spriteRenderer.sprite;
	}
}
