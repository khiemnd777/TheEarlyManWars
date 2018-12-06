using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public PlayerTowerObject playerTowerObject;
    public Transform playerTowerSpawnPoint;
    [Space]
    public MonsterTowerObject monsterTowerObject;
    public Transform monsterTowerSpawnPoint;

    void Start ()
    {
        SpawnPlayerTower();
        SpawnMonsterTower();
    }

    public void SpawnPlayerTower ()
    {
        InstancePlayerTower (playerTowerObject);
    }

    public void SpawnMonsterTower ()
    {
        InstanceMonsterTower (monsterTowerObject);
    }

    PlayerTowerDisplay InstancePlayerTower (PlayerTowerObject playerTowerObject)
    {
        return (PlayerTowerDisplay) Instance (playerTowerSpawnPoint, playerTowerObject);
    }

    MonsterTowerDisplay InstanceMonsterTower (MonsterTowerObject monsterTowerObject)
    {
        return (MonsterTowerDisplay) Instance (monsterTowerSpawnPoint, monsterTowerObject);
    }

    TowerDisplay Instance (Transform spawnPoint, TowerObject towerObject)
    {
        var spawningPosition = spawnPoint.position;
        var instance = Instantiate<TowerDisplay> (towerObject.displayPrefab, spawningPosition, Quaternion.identity);
        instance.towerObject = towerObject;
        return instance;
    }
}
