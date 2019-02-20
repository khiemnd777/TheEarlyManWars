using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
	[SerializeField]
	Currency _currency;
	public Text goldText;
	public Text diamondText;
	public Text experencePointText;

	// Update is called once per frame
	void Update ()
	{
		goldText.text = _currency.gold.ToRoundToInt().ToString();
		diamondText.text = _currency.diamond.ToRoundToInt().ToString();
		experencePointText.text = _currency.experencePoint.ToRoundToInt().ToString();
	}
}
