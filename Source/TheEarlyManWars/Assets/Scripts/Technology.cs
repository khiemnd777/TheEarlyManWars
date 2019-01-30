using UnityEngine;

[CreateAssetMenu (fileName = "Technology", menuName = "Create Technology")]
public class Technology : ScriptableObject
{
    public float meatRate;
    public float upgradedMeatRate;
    public Cost upgradedMeatRateCost;
    [Space]
    public float towerDamageRate;
    public float upgradedTowerDamageRate;
    public Cost upgradedTowerDamageRateCost;
    [Space]
    public float superPowerDamageRate;
    public float upgradedSuperPowerDamageRate;
    public Cost upgradedSuperPowerDamageRateCost;
    [Space]
    public float superPowerCooldownRate;
    public float upgradedSuperPowerCooldownRate;
    public Cost upgradedSuperPowerCooldownRateCost;
    [Space]
    public float meleeDamageRate;
    public float upgradedMeleeDamageRate;
    public Cost upgradedMeleeDamageRateCost;
    [Space]
    public float rangeDamageRate;
    public float upgradedRangeDamageRate;
    public Cost upgradedRangeDamageRateCost;

    public void AddMeatRate ()
    {
        AddMeatRate(upgradedMeatRate);
    }

    public void AddMeatRate (float rate)
    {
        meatRate += rate;
    }

    public void AddTowerDamageRate ()
    {
        AddTowerDamageRate(upgradedTowerDamageRate);
    }

    public void AddTowerDamageRate (float rate)
    {
        towerDamageRate += rate;
    }

    public void AddSuperPowerDamageRate ()
    {
        AddSuperPowerDamageRate(upgradedSuperPowerDamageRate);
    }

    public void AddSuperPowerDamageRate (float rate)
    {
        superPowerDamageRate += rate;
    }

    public void AddSuperPowerCooldownRate ()
    {
        AddSuperPowerCooldownRate(upgradedSuperPowerCooldownRate);
    }

    public void AddSuperPowerCooldownRate (float rate)
    {
        superPowerCooldownRate += rate;
    }

    public void AddMeleeDamageRate ()
    {
        AddMeleeDamageRate(upgradedMeleeDamageRate);
    }

    public void AddMeleeDamageRate (float rate)
    {
        meleeDamageRate += rate;
    }

    public void AddRangeDamageRate ()
    {
        AddRangeDamageRate(upgradedRangeDamageRate);
    }

    public void AddRangeDamageRate (float rate)
    {
        rangeDamageRate += rate;
    }
}
