using UnityEngine;

public class CharacterUpgradeManager : MonoBehaviour
{
    [SerializeField]
    BaseCharacter _baseCharacter;

    public string characterName;
    public float attackPower;
    public float hp;
    public int level;
    public CharacterDisplay display;
    public float upgradedAttackRate;
    public float upgradedHpRate;
    public Cost upgradedCost;

    [SerializeField]
    Currency _currency;

    void Awake ()
    {
        SetBaseCharacterValue (_baseCharacter);
    }

    void SetBaseCharacterValue (BaseCharacter baseCharacter)
    {
        characterName = baseCharacter.name;
        attackPower = baseCharacter.attackPower;
        hp = baseCharacter.hp;
        level = baseCharacter.level;
        display = baseCharacter.displayPrefab;
        upgradedCost = _baseCharacter.upgradedCost;
        upgradedAttackRate = _baseCharacter.upgradedAttackRate;
        upgradedHpRate = _baseCharacter.upgradedHpRate;
    }

    public void ClearBaseCharacter ()
    {
        _baseCharacter = null;
    }

    public void UpgradeByExp (System.Action then)
    {
        if (_baseCharacter == null || _baseCharacter is Object && _baseCharacter.Equals (null)) return;
        _currency.PurchaseByExperencePoint (upgradedCost.ExperencePoint, () =>
        {
            _baseCharacter.attackPower *= (1 + upgradedAttackRate);
            _baseCharacter.hp *= (1 + upgradedHpRate);
            ++_baseCharacter.level;
            _baseCharacter.AssignUpgradedDisplay ();
            attackPower = _baseCharacter.attackPower;
            hp = _baseCharacter.hp;
            level = _baseCharacter.level;
            display = _baseCharacter.displayPrefab;
            var upgradedGoldRate = GetUpgradedCostRate ();
            _baseCharacter.upgradedCost.Gold *= (1 + upgradedGoldRate);
            upgradedCost = _baseCharacter.upgradedCost;
            if (then != null)
            {
                then ();
            }
        });
    }

    public void UpgradeByDiamond (System.Action then)
    {
        if (_baseCharacter == null || _baseCharacter is Object && _baseCharacter.Equals (null)) return;
        _currency.PurchaseByDiamond (upgradedCost.Diamond, () =>
        {
            _baseCharacter.attackPower *= (1 + upgradedAttackRate);
            _baseCharacter.hp *= (1 + upgradedHpRate);
            ++_baseCharacter.level;
            _baseCharacter.AssignUpgradedDisplay ();
            attackPower = _baseCharacter.attackPower;
            hp = _baseCharacter.hp;
            level = _baseCharacter.level;
            display = _baseCharacter.displayPrefab;
            var upgradedGoldRate = GetUpgradedCostRate ();
            _baseCharacter.upgradedCost.Diamond *= (1 + upgradedGoldRate);
            upgradedCost = _baseCharacter.upgradedCost;
            if (then != null)
            {
                then ();
            }
        });
    }

    public int PreviewLevel ()
    {
        return _baseCharacter.level + 1;
    }

    public float PreviewAttackPower ()
    {
        return _baseCharacter.attackPower * (1 + upgradedAttackRate);
    }

    public float PreviewHp ()
    {
        return _baseCharacter.hp * (1 + upgradedHpRate);
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
