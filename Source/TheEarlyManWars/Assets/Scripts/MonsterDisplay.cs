using UnityEngine;

public class MonsterDisplay : ObjectDisplay
{
    public MonsterAttackType attackType;

    public override void Awake ()
    {
        base.Awake ();
        attackType = ((BaseMonster) baseObject).attackType;
        allies = FindObjectOfType<MonsterDisplayList> ();
        enemies = FindObjectOfType<CharacterDisplayList> ();
    }

    public override void Move ()
    {
        if (speed.GetValue () == 0) return;
        transform.position += -Vector3.right * speed.GetValue () * Time.fixedDeltaTime;
    }
}
