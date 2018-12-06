using UnityEngine;

public class MonsterTowerDisplay : TowerDisplay
{
    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        enemies = FindObjectOfType<CharacterDisplayList> ();
        base.Awake();
    }
}
