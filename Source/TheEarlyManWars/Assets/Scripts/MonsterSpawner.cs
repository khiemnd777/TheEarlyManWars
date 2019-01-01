using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float delaySpawning = 5f;
    public float delayNextWave = 1f;
    public Transform spawningPoint;
    public List<Wave> waves;
    [System.NonSerialized]
    public MonsterDisplayList displayList;
    public int waveCount { get { return _waveCount; } }
    Settings _settings;
    List<Wave> _waves;
    Wave _currentWave;
    int _currentMonsterSpawningIndex;
    float _nextWaveTime = 0;
    float _spawnTime = 2f;
    int _waveCount = 0;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
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
        if (_settings.deltaSpeed <= 0) return;
        if (_waveCount == 0)
        {
            _currentWave = ShiftWave ();
            _waveCount++;
            return;
        }
        // if (_currentWave != null && _currentMonsterSpawningIndex == _currentWave.monsters.Count && !displayList.list.Any ())
        if (_currentWave != null && _currentMonsterSpawningIndex == _currentWave.monsters.Count)
        {
            if (_nextWaveTime <= 1f)
            {
                _nextWaveTime += Time.deltaTime / delayNextWave * _settings.deltaSpeed;
                return;
            }
            if (_currentWave != null) _currentWave = null;
            _currentMonsterSpawningIndex = 0;
            _currentWave = ShiftWave ();
            _waveCount++;
            _nextWaveTime = 0f;
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
        if (_settings.deltaSpeed <= 0) return;
        if (_currentWave == null) return;
        if (_currentMonsterSpawningIndex == _currentWave.monsters.Count) return;
        if (_spawnTime <= 1f)
        {
            _spawnTime += Time.deltaTime / delaySpawning * _settings.deltaSpeed;
            return;
        }
        var baseMonster = _currentWave.monsters[_currentMonsterSpawningIndex];
        var mstrDisp = InstanceMonster (baseMonster);
        displayList.Add (mstrDisp);
        ++_currentMonsterSpawningIndex;
        _spawnTime = 0f;
    }

    MonsterDisplay InstanceMonster (BaseMonster baseMonster)
    {
        var z = Random.Range (-1f, 1f);
        var spawningPosition = spawningPoint.position + Vector3.forward * z;
        var instance = Instantiate<MonsterDisplay> (baseMonster.displayPrefab, spawningPosition, Quaternion.identity);
        instance.baseObject = baseMonster;
        return instance;
    }
}
