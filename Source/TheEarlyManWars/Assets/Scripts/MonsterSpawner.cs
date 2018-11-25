using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public int delay = 1;
    public Transform spawningPoint;
    public MonsterDisplayList displayList;
    public List<Wave> waves;
    [SerializeField]
    MonsterDisplay _monsterDisplayPrefab;
    float _spawnTime;
    List<Wave> _waves;
    Wave _currentWave;

    void Awake ()
    {
        displayList = FindObjectOfType<MonsterDisplayList> ();
        // Clone to a wave list to execute.
        _waves = waves.GetRange (0, waves.Count);
    }

    void Update ()
    {
        Spawn ();
    }

    void Spawn ()
    {
        if (Time.time < _spawnTime) return;
        _spawnTime = Time.time + delay;
        if (_currentWave == null)
        {
            _currentWave = ShiftWave ();
        }

    }

    Wave ShiftWave ()
    {
        if (!_waves.Any ()) return null;
        var shift = _waves.First ();
        _waves.RemoveRange (0, 1);
        return shift;
    }

    MonsterDisplay InstanceMonster (BaseMonster baseMonster)
    {
        var instance = Instantiate<MonsterDisplay> (_monsterDisplayPrefab, spawningPoint.position, Quaternion.identity);
        instance.baseObject = baseMonster;
        return instance;
    }
}
