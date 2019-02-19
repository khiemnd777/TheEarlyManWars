using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyUpgradeUI : MonoBehaviour
{
	public TechnologyManager technologyManager;
	[Header ("Technology Value")]
	public Text meatValue;
	public Text towerDamageValue;
	public Text superPowerDamageValue;
	public Text superPowerCooldownValue;
	public Text meleeUnitsDamageValue;
	public Text rangeUnitsDamageValue;
	[Header ("Upgraded Button Text")]
	public Text upgradedMeatGoldText;
	public Text upgradedMeatDiamondText;
	public Text upgradedTowerDamageGoldText;
	public Text upgradedTowerDamageDiamondText;
	public Text upgradedSuperPowerDamageGoldText;
	public Text upgradedSuperPowerDamageDiamondText;
	public Text upgradedSuperPowerCooldownGoldText;
	public Text upgradedSuperPowerCooldownDiamondText;
	public Text upgradedMeleeUnitsGoldText;
	public Text upgradedMeleeUnitsDiamondText;
	public Text upgradedRangeUnitsGoldText;
	public Text upgradedRangeUnitsDiamondText;

	string _formatGold = "{0}G ({1}{2}%)";
	string _formatDiamond = "{0}D ({1}{2}%)";
	string _formatValue = "{0}{1}%";

	void Update ()
	{
		// Technology Value
		meatValue.text = string.Format (_formatValue, "+", technologyManager.meatRate * 100);
		towerDamageValue.text = string.Format (_formatValue, "+", technologyManager.towerDamageRate * 100);
		superPowerDamageValue.text = string.Format (_formatValue, "+", technologyManager.superPowerDamageRate * 100);
		superPowerCooldownValue.text = string.Format (_formatValue, "-", technologyManager.superPowerCooldownRate * 100);
		meleeUnitsDamageValue.text = string.Format (_formatValue, "+", technologyManager.meleeDamageRate * 100);
		rangeUnitsDamageValue.text = string.Format (_formatValue, "+", technologyManager.rangeDamageRate * 100);
		// Upgraded Button Text
		upgradedMeatGoldText.text = string.Format (_formatGold, technologyManager.upgradedMeatRateCost.Gold.ToCeil (), "+", technologyManager.PreviewMeatRate () * 100);
		upgradedMeatDiamondText.text = string.Format (_formatDiamond, technologyManager.upgradedMeatRateCost.Diamond.ToCeil (), "+", technologyManager.PreviewMeatRate () * 100);
		upgradedTowerDamageDiamondText.text = string.Format (_formatDiamond, technologyManager.upgradedMeatRateCost.Diamond.ToCeil (), "+", technologyManager.PreviewMeatRate () * 100);
		upgradedTowerDamageGoldText.text = string.Format (_formatGold, technologyManager.upgradedTowerDamageRateCost.Gold.ToCeil (), "+", technologyManager.PreviewTowerDamageRate () * 100);
		upgradedTowerDamageDiamondText.text = string.Format (_formatDiamond, technologyManager.upgradedTowerDamageRateCost.Diamond.ToCeil (), "+", technologyManager.PreviewTowerDamageRate () * 100);
		upgradedSuperPowerDamageGoldText.text = string.Format (_formatGold, technologyManager.upgradedSuperPowerDamageRateCost.Gold.ToCeil (), "+", technologyManager.PreviewSuperPowerDamageRate () * 100);
		upgradedSuperPowerDamageDiamondText.text = string.Format (_formatDiamond, technologyManager.upgradedSuperPowerDamageRateCost.Diamond.ToCeil (), "+", technologyManager.PreviewSuperPowerDamageRate () * 100);
		upgradedSuperPowerCooldownGoldText.text = string.Format (_formatGold, technologyManager.upgradedSuperPowerCooldownRateCost.Gold.ToCeil (), "-", technologyManager.PreviewSuperPowerCooldownRate () * 100);
		upgradedSuperPowerCooldownDiamondText.text = string.Format (_formatDiamond, technologyManager.upgradedSuperPowerCooldownRateCost.Diamond.ToCeil (), "-", technologyManager.PreviewSuperPowerCooldownRate () * 100);
		upgradedMeleeUnitsGoldText.text = string.Format (_formatGold, technologyManager.upgradedMeleeDamageRateCost.Gold.ToCeil (), "+", technologyManager.PreviewMeleeDamageRate () * 100);
		upgradedMeleeUnitsDiamondText.text = string.Format (_formatDiamond, technologyManager.upgradedMeleeDamageRateCost.Diamond.ToCeil (), "+", technologyManager.PreviewMeleeDamageRate () * 100);
		upgradedRangeUnitsGoldText.text = string.Format (_formatGold, technologyManager.upgradedRangeDamageRateCost.Gold.ToCeil (), "+", technologyManager.PreviewRangeDamageRate () * 100);
		upgradedRangeUnitsDiamondText.text = string.Format (_formatDiamond, technologyManager.upgradedRangeDamageRateCost.Diamond.ToCeil (), "+", technologyManager.PreviewRangeDamageRate () * 100);
	}
}
