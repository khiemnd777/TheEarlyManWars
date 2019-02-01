using UnityEngine;

[CreateAssetMenu(fileName="Player Tower", menuName="Create Player Tower")]
public class PlayerTowerObject : TowerObject
{
    [Header("Upgrade")]
    public float upgradedAttackRate;
    public float upgradedRangeRate;
    public float upgradedDefendRate;
    public Cost upgradedCost;
}
