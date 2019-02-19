using UnityEngine;

[CreateAssetMenu (fileName = "Technology", menuName = "Create Technology")]
public class Technology : ScriptableObject
{
    public float meatRate;
    public int meatRateLevel;
    public float upgradedMeatRate;
    public Cost upgradedMeatRateCost;
    [Space]
    public float towerDamageRate;
    public int towerDamageRateLevel;
    public float upgradedTowerDamageRate;
    public Cost upgradedTowerDamageRateCost;
    [Space]
    public float superPowerDamageRate;
    public int superPowerDamageRateLevel;
    public float upgradedSuperPowerDamageRate;
    public Cost upgradedSuperPowerDamageRateCost;
    [Space]
    public float superPowerCooldownRate;
    public int superPowerCooldownRateLevel;
    public float upgradedSuperPowerCooldownRate;
    public Cost upgradedSuperPowerCooldownRateCost;
    [Space]
    public float meleeDamageRate;
    public int meleeDamageRateLevel;
    public float upgradedMeleeDamageRate;
    public Cost upgradedMeleeDamageRateCost;
    [Space]
    public float rangeDamageRate;
    public int rangeDamageRateLevel;
    public float upgradedRangeDamageRate;
    public Cost upgradedRangeDamageRateCost;

    public void AddMeatRate ()
    {
        AddMeatRate (upgradedMeatRate);
    }

    public void AddMeatRate (float rate)
    {
        meatRate += rate;
    }

    public float PreviewMeatRate ()
    {
        return meatRate + upgradedMeatRate;
    }

    public void AddTowerDamageRate ()
    {
        AddTowerDamageRate (upgradedTowerDamageRate);
    }

    public void AddTowerDamageRate (float rate)
    {
        towerDamageRate += rate;
    }

    public float PreviewTowerDamageRate ()
    {
        return towerDamageRate + upgradedTowerDamageRate;
    }

    public void AddSuperPowerDamageRate ()
    {
        AddSuperPowerDamageRate (upgradedSuperPowerDamageRate);
    }

    public void AddSuperPowerDamageRate (float rate)
    {
        superPowerDamageRate += rate;
    }

    public float PreviewSuperPowerDamageRate ()
    {
        return superPowerDamageRate + upgradedSuperPowerDamageRate;
    }

    public void AddSuperPowerCooldownRate ()
    {
        AddSuperPowerCooldownRate (upgradedSuperPowerCooldownRate);
    }

    public void AddSuperPowerCooldownRate (float rate)
    {
        superPowerCooldownRate += rate;
    }

    public float PreviewSuperPowerCooldownRate ()
    {
        return superPowerCooldownRate + upgradedSuperPowerCooldownRate;
    }

    public void AddMeleeDamageRate ()
    {
        AddMeleeDamageRate (upgradedMeleeDamageRate);
    }

    public void AddMeleeDamageRate (float rate)
    {
        meleeDamageRate += rate;
    }

    public float PreviewMeleeDamageRate ()
    {
        return meleeDamageRate + upgradedMeleeDamageRate;
    }

    public void AddRangeDamageRate ()
    {
        AddRangeDamageRate (upgradedRangeDamageRate);
    }

    public void AddRangeDamageRate (float rate)
    {
        rangeDamageRate += rate;
    }

    public float PreviewRangeDamageRate ()
    {
        return rangeDamageRate + upgradedRangeDamageRate;
    }
}
