using System.Linq;
using UnityEngine;

public class TowerUpgradeManager : MonoBehaviour
{
    [SerializeField]
    PlayerTowerObject _towerObject;
    [SerializeField]
    Currency _currency;

    public float damage;
    public float rangeAttack;
    public float hp;
    public float upgradedAttackRate;
    public float upgradedRangeRate;
    public float upgradedDefendRate;
    public Cost upgradedCost;

    void Start ()
    {
        SetTowerObjectAsDefault ();
    }

    public void SetTowerObjectAsDefault ()
    {
        damage = _towerObject.damage;
        rangeAttack = _towerObject.rangeAttack;
        hp = _towerObject.hp;
        upgradedCost = _towerObject.upgradedCost;
        upgradedAttackRate = _towerObject.upgradedAttackRate;
        upgradedRangeRate = _towerObject.upgradedRangeRate;
        upgradedDefendRate = _towerObject.upgradedDefendRate;
    }

    public void SetTowerObject (PlayerTowerObject towerObject)
    {
        _towerObject = towerObject;
        damage = _towerObject.damage;
        rangeAttack = _towerObject.rangeAttack;
        hp = _towerObject.hp;
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
            damage = _towerObject.damage;
            rangeAttack = _towerObject.rangeAttack;
            hp = _towerObject.hp;
            ++_towerObject.level;
            _towerObject.AssignUpgradedDisplay ();
            _towerObject.upgradedCost.Gold *= 1.2f;
            upgradedCost = _towerObject.upgradedCost;
        });
    }

    public void UpgradeByDiamond ()
    {
        if (_towerObject == null || _towerObject is Object && _towerObject.Equals (null)) return;
        _currency.PurchaseByDiamond (upgradedCost.Diamond, () =>
        {
            _towerObject.damage *= (1 + upgradedAttackRate);
            _towerObject.rangeAttack *= (1 + upgradedRangeRate);
            _towerObject.hp *= (1 + upgradedDefendRate);
            damage = _towerObject.damage;
            rangeAttack = _towerObject.rangeAttack;
            hp = _towerObject.hp;
            ++_towerObject.level;
            _towerObject.AssignUpgradedDisplay ();
            _towerObject.upgradedCost.Diamond *= 1.2f;
            upgradedCost = _towerObject.upgradedCost;
        });
    }

    public float PreviewDamageRate ()
    {
        return _towerObject.damage * (1 + upgradedAttackRate);
    }

    public float PreviewAttackRangeRate ()
    {
        return _towerObject.rangeAttack * (1 + upgradedRangeRate);
    }

    public float PreviewHpRate ()
    {
        return _towerObject.hp * (1 + upgradedDefendRate);
    }
}
