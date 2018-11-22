using UnityEngine;

public class CharacterDisplay : ObjectDisplay
{
    public override void Awake(){
        base.Awake();
        allies = FindObjectOfType<CharacterDisplayList>();
        enemies = FindObjectOfType<MonsterDisplayList>();
    }
}