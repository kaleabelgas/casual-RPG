using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static List<GameObject> enemiesActive = new List<GameObject>();
    private static List<GameObject> bulletsActive = new List<GameObject>();

    public static void AddBulletToList(GameObject _bullet)
    {
        bulletsActive.Add(_bullet);
    }
    public static void RemoveBulletFromList(GameObject _bullet)
    {
        bulletsActive.Remove(_bullet);
    }
    public static void AddEnemyToList(GameObject _enemy)
    {
        enemiesActive.Add(_enemy);
        //Debug.Log("Adding " + _enemy.name);
    }
    public static void RemoveEnemyFromList(GameObject _enemy)
    {
        enemiesActive.Remove(_enemy);
        //Debug.Log("Removing " + _enemy.name);
    }
    public static bool LookForEnemies()
    {
        return enemiesActive.Count > 0;
    }
    public static List<GameObject> GetAllActiveEnemies()
    {
        return enemiesActive;
    }
}
