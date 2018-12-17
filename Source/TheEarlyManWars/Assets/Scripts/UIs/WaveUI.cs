using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    MonsterSpawner _monsterSpawner;
    [SerializeField]
    Text _value;

    void Start ()
    {
        _monsterSpawner = FindObjectOfType<MonsterSpawner> ();
    }

    void Update ()
    {
        _value.text = _monsterSpawner.waveCount.ToString();
    }
}
