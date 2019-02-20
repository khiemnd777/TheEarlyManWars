using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
	public TowerUpgradeManager towerUpgradeManager;
	[Header ("Values")]
	public Text attackRateText;
	public Text attackRangeRateText;
	public Text defendRateText;
	[Header ("Preview")]
	public Text previewAttackRateText;
	public Text previewAttackRangeRateText;
	public Text previewDefendRateText;
	[Header ("Buttons")]
	public Text upgradedGoldText;
	public Text upgradedDiamondText;

	string _formatValue = "{0}";
	string _formatGold = "Upgrade ({0}G)";
	string _formatDiamond = "Upgrade ({0}D)";

	void Update ()
	{
		// values
		attackRateText.text = towerUpgradeManager.damage.ToRoundToInt ().ToString ();
		attackRangeRateText.text = towerUpgradeManager.rangeAttack.ToRoundToInt ().ToString ();
		defendRateText.text = towerUpgradeManager.hp.ToRoundToInt ().ToString ();
		// preview
		previewAttackRateText.text = towerUpgradeManager.PreviewDamageRate ().ToRoundToInt ().ToString ();
		previewAttackRangeRateText.text = towerUpgradeManager.PreviewAttackRangeRate ().ToRoundToInt ().ToString ();
		previewDefendRateText.text = towerUpgradeManager.PreviewHpRate ().ToRoundToInt ().ToString ();
		// buttons
		upgradedGoldText.text = string.Format (_formatGold, towerUpgradeManager.upgradedCost.Gold.ToRoundToInt ());
		upgradedDiamondText.text = string.Format (_formatDiamond, towerUpgradeManager.upgradedCost.Diamond.ToRoundToInt ());
	}
}
