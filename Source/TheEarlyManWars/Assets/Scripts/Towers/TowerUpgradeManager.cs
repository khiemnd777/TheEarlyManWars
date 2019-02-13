using System.Linq;
using UnityEngine;

public class TowerUpgradeManager : MonoBehaviour
{
    PlayerTowerObject _towerObject;
    [SerializeField]
    Currency _currency;

    public float upgradedAttackRate;
    public float upgradedRangeRate;
    public float upgradedDefendRate;
    public Cost upgradedCost;

    public void SetTowerObject (PlayerTowerObject towerObject)
    {
        _towerObject = towerObject;
        upgradedCost = _towerObject.upgradedCost;
        upgradedAttackRate = _towerObject.upgradedAttackRate;
        upgradedRangeRate = _towerObject.upgradedRangeRate;
        upgradedDefendRate = _towerObject.upgradedDefendRate;
    }

    public void ClearTowerObject ()
    {
        _towerObject = null;
    }

    public void UpgradeByGold ()
    {
        if (_towerObject == null || _towerObject is Object && _towerObject.Equals (null)) return;
        _currency.PurchaseByGold (upgradedCost.Gold, () =>
        {
            _towerObject.damage *= (1 + upgradedAttackRate);
            _towerObject.rangeAttack *= (1 + upgradedRangeRate);
            _towerObject.hp *= (1 + upgradedDefendRate);
            ++_towerObject.level;
            _towerObject.AssignUpgradedAnimator();
            _towerObject.upgradedCost.Gold *= 1.2f;
            upgradedCost = _towerObject.upgradedCost;
        });
    }

    public void UpgradeByDiamond ()
    {
        if (_towerObject == null || _towerObject is Object && _towerObject.Equals (null)) return;
        _currency.PurchaseByGold (upgradedCost.Diamond, () =>
        {
            _towerObject.damage *= (1 + upgradedAttackRate);
            _towerObject.rangeAttack *= (1 + upgradedRangeRate);
            _towerObject.hp *= (1 + upgradedDefendRate);
            ++_towerObject.level;
            _towerObject.AssignUpgradedAnimator();
            _towerObject.upgradedCost.Diamond *= 1.2f;
            upgradedCost = _towerObject.upgradedCost;
        });
    }
}
