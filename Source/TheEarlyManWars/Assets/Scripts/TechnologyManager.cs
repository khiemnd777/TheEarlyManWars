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
    public Cost towerDamageRateCost;
    [Space]
    public float superPowerDamageRate;
    public Cost superPowerDamageRateCost;
    [Space]
    public float superPowerCooldownRate;
    public Cost superPowerCooldownRateCost;
    [Space]
    public float meleeDamageRate;
    public Cost meleeDamageRateCost;
    [Space]
    public float rangeDamageRate;
    public Cost rangeDamageRateCost;

    void Start ()
    {
        meatRate = _technology.meatRate;
        upgradedMeatRateCost = _technology.upgradedMeatRateCost;
        towerDamageRate = _technology.towerDamageRate;
        towerDamageRateCost = _technology.upgradedTowerDamageRateCost;
        superPowerDamageRate = _technology.superPowerDamageRate;
        superPowerDamageRateCost = _technology.upgradedSuperPowerDamageRateCost;
        superPowerCooldownRate = _technology.superPowerCooldownRate;
        superPowerCooldownRateCost = _technology.upgradedSuperPowerCooldownRateCost;
        meleeDamageRate = _technology.meleeDamageRate;
        meleeDamageRateCost = _technology.upgradedMeleeDamageRateCost;
        rangeDamageRate = _technology.rangeDamageRate;
        rangeDamageRateCost = _technology.upgradedRangeDamageRateCost;
    }

    public void UpgradeMeatRateByGold ()
    {
        _currency.PurchaseByGold (upgradedMeatRateCost.Gold, () =>
        {
            _technology.AddMeatRate(_technology.upgradedMeatRate);
            meatRate = _technology.meatRate;
            _technology.upgradedMeatRateCost.Gold *= 1.2f;
            upgradedMeatRateCost = _technology.upgradedMeatRateCost;
        });
    }

    public void UpgradeMeatRateByDiamond ()
    {
        _currency.PurchaseByGold (upgradedMeatRateCost.Diamond, () =>
        {
            _technology.AddMeatRate(_technology.upgradedMeatRate);
            meatRate = _technology.meatRate;
            _technology.upgradedMeatRateCost.Diamond *= 1.2f;
            upgradedMeatRateCost = _technology.upgradedMeatRateCost;
        });
    }
}
