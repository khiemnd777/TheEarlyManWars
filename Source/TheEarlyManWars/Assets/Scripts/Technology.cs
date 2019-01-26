using UnityEngine;

[CreateAssetMenu (fileName = "Technology", menuName = "Create Technology")]
public class Technology : ScriptableObject
{
    public float meatRate;
    public float towerDamageRate;
    public float superPowerDamageRate;
    public float superPowerCooldownRate;
    public float meleeDamageRate;
    public float rangeDamageRate;

    public void AddMeatRate (float rate)
    {
        meatRate += rate;
    }

    public void AddTowerDamageRate (float rate)
    {
        towerDamageRate += rate;
    }

    public void AddSuperPowerDamageRate (float rate)
    {
        superPowerDamageRate += rate;
    }

    public void AddSuperPowerCooldownRate (float rate)
    {
        superPowerCooldownRate += rate;
    }

    public void AddMeleeDamageRate (float rate)
    {
        meleeDamageRate += rate;
    }

    public void AddRangeDamageRate (float rate)
    {
        rangeDamageRate += rate;
    }
}
