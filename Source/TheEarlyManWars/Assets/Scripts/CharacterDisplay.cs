using UnityEngine;

public class CharacterDisplay : ObjectDisplay
{
    public AttackType attackType;

    public override void Awake ()
    {
        base.Awake ();
        attackType = ((BaseCharacter) baseObject).attackType;
        allies = FindObjectOfType<CharacterDisplayList> ();
        enemies = FindObjectOfType<MonsterDisplayList> ();
    }

    public override void Move ()
    {
        if (speed.GetValue () == 0) return;
        transform.position += Vector3.right * speed.GetValue () * Time.fixedDeltaTime;
    }
}
