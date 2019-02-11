using UnityEngine;

public class CharacterUpgradeManager : MonoBehaviour
{
    BaseCharacter _baseCharacter;

    public float upgradedAttackRate;
    public float upgradedHpRate;
    public Cost upgradedCost;

    [SerializeField]
    Currency _currency;

    public void SetBaseCharacter (BaseCharacter baseCharacter)
    {
        _baseCharacter = baseCharacter;
        upgradedCost = _baseCharacter.upgradedCost;
        upgradedAttackRate = _baseCharacter.upgradedAttackRate;
        upgradedHpRate = _baseCharacter.upgradedHpRate;
    }

    public void ClearBaseCharacter ()
    {
        _baseCharacter = null;
    }

    public void UpgradeByGold ()
    {
        if (_baseCharacter == null || _baseCharacter is Object && _baseCharacter.Equals (null)) return;
        _currency.PurchaseByGold (upgradedCost.Gold, () =>
        {
            _baseCharacter.attackPower *= (1 + upgradedAttackRate);
            _baseCharacter.hp *= (1 + upgradedHpRate);
            ++_baseCharacter.level;
            var upgradedGoldRate = GetUpgradedCostRate();
            _baseCharacter.upgradedCost.Gold *= (1 + upgradedGoldRate);
            upgradedCost = _baseCharacter.upgradedCost;
        });
    }

    public void UpgradeByDiamond ()
    {
        if (_baseCharacter == null || _baseCharacter is Object && _baseCharacter.Equals (null)) return;
        _currency.PurchaseByGold (upgradedCost.Diamond, () =>
        {
            _baseCharacter.attackPower *= (1 + upgradedAttackRate);
            _baseCharacter.hp *= (1 + upgradedHpRate);
            ++_baseCharacter.level;
            var upgradedGoldRate = GetUpgradedCostRate();
            _baseCharacter.upgradedCost.Diamond *= (1 + upgradedGoldRate);
            upgradedCost = _baseCharacter.upgradedCost;
        });
    }

    float GetUpgradedCostRate ()
    {
        var upgradedGoldRate = .2f;
        if (_baseCharacter.characterLevel == CharacterLevel.Super)
        {
            upgradedGoldRate *= 2f;
        }
        else if (_baseCharacter.characterLevel == CharacterLevel.Champion)
        {
            upgradedGoldRate *= 3f;
        }
        else if (_baseCharacter.characterLevel == CharacterLevel.Legendary)
        {
            upgradedGoldRate *= 4f;
        }
        return upgradedGoldRate;
    }
}
