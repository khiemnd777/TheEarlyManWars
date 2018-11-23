using UnityEngine;

public class MonsterDisplay : ObjectDisplay
{
    public MonsterAttackType attackType;

    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        base.Awake ();
        attackType = ((BaseMonster) baseObject).attackType;
        allies = FindObjectOfType<MonsterDisplayList> ();
        enemies = FindObjectOfType<CharacterDisplayList> ();
    }
}
