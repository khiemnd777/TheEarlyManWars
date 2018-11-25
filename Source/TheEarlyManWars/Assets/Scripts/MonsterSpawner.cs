using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public int delaySpawning = 1;
    public int delayNextWave = 5;
    public Transform spawningPoint;
    public MonsterDisplayList displayList;
    public List<Wave> waves;
    [SerializeField]
    MonsterDisplay _monsterDisplayPrefab;
    List<Wave> _waves;
    Wave _currentWave;
    int _currentMonsterSpawningIndex;
    float _nextWaveTime;
    float _spawnTime;
    int _waveCount = 0;

    void Awake ()
    {
        displayList = FindObjectOfType<MonsterDisplayList> ();
        // Clone to a wave list to execute.
        _waves = waves.GetRange (0, waves.Count);
    }

    void Update ()
    {
        NextWave ();
        Spawn ();
    }

    void NextWave ()
    {
        if (_waveCount == 0)
        {
            _currentWave = ShiftWave ();
            _waveCount++;
            return;
        }
        if (_currentWave != null && _currentMonsterSpawningIndex == _currentWave.monsters.Count)
        {
            if (_currentWave != null) _currentWave = null;
            _currentMonsterSpawningIndex = 0;
            if (Time.time < _nextWaveTime) return;
            _nextWaveTime = Time.time + delayNextWave;
            _currentWave = ShiftWave ();
            _waveCount++;
        }
    }

    Wave ShiftWave ()
    {
        if (!_waves.Any ()) return null;
        var shift = _waves.First ();
        _waves.RemoveRange (0, 1);
        return shift;
    }

    void Spawn ()
    {
        if (_currentWave == null) return;
        if (_currentMonsterSpawningIndex == _currentWave.monsters.Count) return;
        if (Time.time < _spawnTime) return;
        _spawnTime = Time.time + delaySpawning;
        var baseMonster = _currentWave.monsters[_currentMonsterSpawningIndex];
        var mstrDisp = InstanceMonster (baseMonster);
        displayList.Add (mstrDisp);
        _currentMonsterSpawningIndex++;
    }

    MonsterDisplay InstanceMonster (BaseMonster baseMonster)
    {
        var z = Random.Range (-1f, 1f);
        var spawningPosition = spawningPoint.position + Vector3.forward * z;
        var instance = Instantiate<MonsterDisplay> (_monsterDisplayPrefab, spawningPosition, Quaternion.identity);
        instance.baseObject = baseMonster;
        return instance;
    }
}
