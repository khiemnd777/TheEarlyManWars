using UnityEngine;

public class TechnologyManager : MonoBehaviour
{
    [SerializeField]
    Technology _technology;

    public float meatRate;
    public float towerDamageRate;
    public float superPowerDamageRate;
    public float superPowerCooldownRate;
    public float meleeDamageRate;
    public float rangeDamageRate;

    void Start ()
    {
        meatRate = _technology.meatRate;   
        towerDamageRate = _technology.towerDamageRate;
        superPowerDamageRate = _technology.superPowerDamageRate;
        superPowerCooldownRate = _technology.superPowerCooldownRate;
        meleeDamageRate = _technology.meleeDamageRate;
        rangeDamageRate = _technology.rangeDamageRate;
    }
}
