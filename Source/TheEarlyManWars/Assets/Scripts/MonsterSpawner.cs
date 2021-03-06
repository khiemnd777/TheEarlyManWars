using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float delaySpawning = 5f;
    public float delayNextWave = 1f;
    public Transform spawningPoint;
    public float bossSpawnKnockBackRange = 2f;
    public List<Wave> waves;
    [System.NonSerialized]
    public MonsterDisplayList displayList;
    [System.NonSerialized]
    public CharacterDisplayList characterDisplayList;
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
        characterDisplayList = FindObjectOfType<CharacterDisplayList> ();
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
        // if (_currentWave != null && _currentMonsterSpawningIndex == _currentWave.monsters.Count && !displayList.list.Any ())
        if (_currentWave != null && _currentMonsterSpawningIndex == _currentWave.monsters.Count)
        {
            if (_nextWaveTime <= 1f)
            {
                _nextWaveTime += Time.deltaTime / delayNextWave;
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
        if (_currentWave == null) return;
        if (_currentMonsterSpawningIndex == _currentWave.monsters.Count) return;
        if (_spawnTime <= 1f)
        {
            _spawnTime += Time.deltaTime / delaySpawning;
            return;
        }
        var baseMonster = _currentWave.monsters[_currentMonsterSpawningIndex];
        StartCoroutine (KnockBackOfSpawnBoss (baseMonster));
        var mstrDisp = InstanceMonster (baseMonster);
        displayList.Add (mstrDisp);
        ++_currentMonsterSpawningIndex;
        _spawnTime = 0f;
    }

    IEnumerator KnockBackOfSpawnBoss (BaseMonster monster)
    {
        if (monster.attackType != MonsterAttackType.Boss) yield break;
        var charactersNearbyTower = characterDisplayList.list.Where (x => Mathf.Abs (x.transform.position.x - spawningPoint.position.x) <= bossSpawnKnockBackRange).ToList ();
        if (!charactersNearbyTower.Any ()) yield break;
        charactersNearbyTower.ForEach (x => x.StopMove ());
        var targetPos = charactersNearbyTower.Select (x => new Vector2 (x.transform.position.x - bossSpawnKnockBackRange, x.transform.position.y)).ToList ();
        var originPos = charactersNearbyTower.Select (x => x.transform.position).ToList ();
        var percent = 0f;
        while (percent <= 1f)
        {
            var step = Time.deltaTime * _settings.deltaMoveStep * 12f;
            percent += step;
            for (var i = 0; i < charactersNearbyTower.Count; i++)
            {
                var character = charactersNearbyTower[i];
                character.transform.position = Vector2.Lerp (originPos[i], targetPos[i], percent);
            }
            yield return null;
        }
        charactersNearbyTower.ForEach (x => x.CanMove ());
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
