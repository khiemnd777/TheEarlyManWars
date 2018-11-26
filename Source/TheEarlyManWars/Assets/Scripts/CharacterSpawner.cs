using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform spawningPoint;
    [System.NonSerialized]
    public CharacterDisplayList displayList;
    [System.NonSerialized]
    public Settings settings;
    [SerializeField]
    CharacterDisplay _displayPrefab;

    void Awake ()
    {
        settings = FindObjectOfType<Settings> ();
        displayList = FindObjectOfType<CharacterDisplayList> ();
    }

    public void Spawn (BaseCharacter baseCharacter)
    {
        
    }
}
