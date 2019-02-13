using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = "Player Tower", menuName = "Create Player Tower")]
public class PlayerTowerObject : TowerObject
{
    [Header ("Upgrade")]
    public float upgradedAttackRate;
    public float upgradedRangeRate;
    public float upgradedDefendRate;
    public Cost upgradedCost;
    public UpgradedAnimationLevel[] upgradedAnimationLevels;

    public void AssignUpgradedAnimator ()
    {
        if (upgradedAnimationLevels.All (x => x.level != level)) return;
        var mUpgradedAnimationLevel = upgradedAnimationLevels.FirstOrDefault (x => x.level == level);
        animator = mUpgradedAnimationLevel.animator;
    }
}
