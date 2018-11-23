using UnityEngine;

public class CharacterDisplay : ObjectDisplay
{
    public AttackType attackType;

    public override void Awake ()
    {
        direction = Direction.LeftToRight;
        base.Awake ();
        attackType = ((BaseCharacter) baseObject).attackType;
        allies = FindObjectOfType<CharacterDisplayList> ();
        enemies = FindObjectOfType<MonsterDisplayList> ();
    }
}
