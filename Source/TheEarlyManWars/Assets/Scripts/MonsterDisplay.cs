using UnityEngine;

public class MonsterDisplay : ObjectDisplay
{
    public override void Awake()
    {
        base.Awake();
        allies = FindObjectOfType<MonsterDisplayList>();
        enemies = FindObjectOfType<CharacterDisplayList>();
    }
}