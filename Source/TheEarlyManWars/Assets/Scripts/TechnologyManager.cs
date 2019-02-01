using UnityEngine;

public class TechnologyManager : MonoBehaviour
{
    [SerializeField]
    Technology _technology;
    [SerializeField]
    Currency _currency;
    [Space]
    public float meatRate;
    public Cost upgradedMeatRateCost;
    [Space]
    public float towerDamageRate;
    public Cost upgradedTowerDamageRateCost;
    [Space]
    public float superPowerDamageRate;
    public Cost upgradedSuperPowerDamageRateCost;
    [Space]
    public float superPowerCooldownRate;
    public Cost upgradedSuperPowerCooldownRateCost;
    [Space]
    public float meleeDamageRate;
    public Cost upgradedMeleeDamageRateCost;
    [Space]
    public float rangeDamageRate;
    public Cost upgradedRangeDamageRateCost;

    void Start ()
    {
        meatRate = _technology.meatRate;
        upgradedMeatRateCost = _technology.upgradedMeatRateCost;
        towerDamageRate = _technology.towerDamageRate;
        upgradedTowerDamageRateCost = _technology.upgradedTowerDamageRateCost;
        superPowerDamageRate = _technology.superPowerDamageRate;
        upgradedSuperPowerDamageRateCost = _technology.upgradedSuperPowerDamageRateCost;
        superPowerCooldownRate = _technology.superPowerCooldownRate;
        upgradedSuperPowerCooldownRateCost = _technology.upgradedSuperPowerCooldownRateCost;
        meleeDamageRate = _technology.meleeDamageRate;
        upgradedMeleeDamageRateCost = _technology.upgradedMeleeDamageRateCost;
        rangeDamageRate = _technology.rangeDamageRate;
        upgradedRangeDamageRateCost = _technology.upgradedRangeDamageRateCost;
    }

    public void UpgradeMeatRateByGold ()
    {
        _currency.PurchaseByGold (upgradedMeatRateCost.Gold, () =>
        {
            _technology.AddMeatRate();
            ++_technology.meatRateLevel;
            meatRate = _technology.meatRate;
            _technology.upgradedMeatRateCost.Gold *= 1.2f;
            upgradedMeatRateCost = _technology.upgradedMeatRateCost;
        });
    }

    public void UpgradeMeatRateByDiamond ()
    {
        _currency.PurchaseByGold (upgradedMeatRateCost.Diamond, () =>
        {
            _technology.AddMeatRate();
            ++_technology.meatRateLevel;
            meatRate = _technology.meatRate;
            _technology.upgradedMeatRateCost.Diamond *= 1.2f;
            upgradedMeatRateCost = _technology.upgradedMeatRateCost;
        });
    }

    public void UpgradeTowerDamageRateByGold ()
    {
        _currency.PurchaseByGold (upgradedTowerDamageRateCost.Gold, () =>
        {
            _technology.AddTowerDamageRate();
            ++_technology.towerDamageRateLevel;
            towerDamageRate = _technology.towerDamageRate;
            _technology.upgradedTowerDamageRateCost.Gold *= 1.2f;
            upgradedTowerDamageRateCost = _technology.upgradedTowerDamageRateCost;
        });
    }

    public void UpgradeTowerDamageRateByDiamond ()
    {
        _currency.PurchaseByGold (upgradedTowerDamageRateCost.Diamond, () =>
        {
            _technology.AddTowerDamageRate();
            ++_technology.towerDamageRateLevel;
            towerDamageRate = _technology.towerDamageRate;
            _technology.upgradedTowerDamageRateCost.Diamond *= 1.2f;
            upgradedTowerDamageRateCost = _technology.upgradedTowerDamageRateCost;
        });
    }

    public void UpgradeSuperPowerDamageRateByGold ()
    {
        _currency.PurchaseByGold (upgradedSuperPowerDamageRateCost.Gold, () =>
        {
            _technology.AddSuperPowerDamageRate();
            ++_technology.superPowerDamageRateLevel;
            superPowerDamageRate = _technology.superPowerDamageRate;
            _technology.upgradedSuperPowerDamageRateCost.Gold *= 1.2f;
            upgradedSuperPowerDamageRateCost = _technology.upgradedSuperPowerDamageRateCost;
        });
    }

    public void UpgradeSuperPowerDamageRateByDiamond ()
    {
        _currency.PurchaseByGold (upgradedSuperPowerDamageRateCost.Diamond, () =>
        {
            _technology.AddSuperPowerDamageRate();
            ++_technology.superPowerDamageRateLevel;
            superPowerDamageRate = _technology.superPowerDamageRate;
            _technology.upgradedSuperPowerDamageRateCost.Diamond *= 1.2f;
            upgradedSuperPowerDamageRateCost = _technology.upgradedSuperPowerDamageRateCost;
        });
    }

    public void UpgradeSuperPowerCooldownRateByGold ()
    {
        _currency.PurchaseByGold (upgradedSuperPowerCooldownRateCost.Gold, () =>
        {
            _technology.AddSuperPowerCooldownRate();
            ++_technology.superPowerCooldownRateLevel;
            superPowerCooldownRate = _technology.superPowerCooldownRate;
            _technology.upgradedSuperPowerCooldownRateCost.Gold *= 1.2f;
            upgradedSuperPowerCooldownRateCost = _technology.upgradedSuperPowerCooldownRateCost;
        });
    }

    public void UpgradeSuperPowerCooldownRateByDiamond ()
    {
        _currency.PurchaseByGold (upgradedSuperPowerCooldownRateCost.Diamond, () =>
        {
            _technology.AddSuperPowerCooldownRate();
            ++_technology.superPowerCooldownRateLevel;
            superPowerCooldownRate = _technology.superPowerCooldownRate;
            _technology.upgradedSuperPowerCooldownRateCost.Diamond *= 1.2f;
            upgradedSuperPowerCooldownRateCost = _technology.upgradedSuperPowerCooldownRateCost;
        });
    }

    public void UpgradeMeleeDamagenRateByGold ()
    {
        _currency.PurchaseByGold (upgradedMeleeDamageRateCost.Gold, () =>
        {
            _technology.AddMeleeDamageRate();
            ++_technology.meleeDamageRateLevel;
            meleeDamageRate = _technology.meleeDamageRate;
            _technology.upgradedMeleeDamageRateCost.Gold *= 1.2f;
            upgradedMeleeDamageRateCost = _technology.upgradedMeleeDamageRateCost;
        });
    }

    public void UpgradeMeleeDamagenRateByDiamond ()
    {
        _currency.PurchaseByGold (upgradedMeleeDamageRateCost.Diamond, () =>
        {
            _technology.AddMeleeDamageRate();
            ++_technology.meleeDamageRateLevel;
            meleeDamageRate = _technology.meleeDamageRate;
            _technology.upgradedMeleeDamageRateCost.Diamond *= 1.2f;
            upgradedMeleeDamageRateCost = _technology.upgradedMeleeDamageRateCost;
        });
    }

    public void UpgradeRangeDamagenRateByGold ()
    {
        _currency.PurchaseByGold (upgradedRangeDamageRateCost.Gold, () =>
        {
            _technology.AddRangeDamageRate();
            ++_technology.rangeDamageRateLevel;
            rangeDamageRate = _technology.rangeDamageRate;
            _technology.upgradedRangeDamageRateCost.Gold *= 1.2f;
            upgradedRangeDamageRateCost = _technology.upgradedRangeDamageRateCost;
        });
    }

    public void UpgradeRangeDamagenRateByDiamond ()
    {
        _currency.PurchaseByGold (upgradedRangeDamageRateCost.Diamond, () =>
        {
            _technology.AddRangeDamageRate();
            ++_technology.rangeDamageRateLevel;
            rangeDamageRate = _technology.rangeDamageRate;
            _technology.upgradedRangeDamageRateCost.Diamond *= 1.2f;
            upgradedRangeDamageRateCost = _technology.upgradedRangeDamageRateCost;
        });
    }
}
