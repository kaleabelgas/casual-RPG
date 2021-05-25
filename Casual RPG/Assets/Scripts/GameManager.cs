using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum ObjectLists
    {
        bullet,
        enemy
    }
    public static GameManager Instance;
    private Dictionary<ObjectLists, List<GameObject>> listOfObjects;


    private void Awake()
    {
        Instance = this;

        listOfObjects = new Dictionary<ObjectLists, List<GameObject>>();

        foreach (var objectList in (ObjectLists[]) Enum.GetValues(typeof(ObjectLists)))
        {
            List<GameObject> gameobjects = new List<GameObject>();
            listOfObjects.Add(objectList, gameobjects);
        }
    }


    public void AddObjectToList(ObjectLists _list, GameObject _object)
    {
        listOfObjects[_list].Add(_object);
    }

    public void RemoveObjectFromList(ObjectLists _list, GameObject _object)
    {
        listOfObjects[_list].Remove(_object);
    }

    public List<GameObject> GetAllObjectsAsList(ObjectLists _list)
    {
        return listOfObjects[_list];
    }

    public bool CheckObjectsInList(ObjectLists _list)
    {
        return listOfObjects[_list].Count > 0;
    }

    public Transform GetClosestObject(ObjectLists _list, Transform _transform, float _searchRadius)
    {
        if (!CheckObjectsInList(_list)) { return null; }

        List<GameObject> _targetObjects = listOfObjects[_list];

        Transform _targetTransform = null;

        float _distanceToObject;

        float _shortestDistance = _searchRadius;

        for (int i = 0; i < _targetObjects.Count; i++)
        {
            _distanceToObject = Vector2.Distance(_targetObjects[i].transform.position, _transform.position);

            if (_distanceToObject < _shortestDistance)
            {
                _shortestDistance = _distanceToObject;
                _targetTransform = _targetObjects[i].transform;
            }
        }
        return _targetTransform;
    }

    public void StartWinGame()
    {
        Debug.Log("Game Won!");
        StartCoroutine(CheckWinGame());
    }

    public void LoseGame()
    {
        Debug.Log("GAME LOST");
        StopCoroutine(CheckWinGame());
        Time.timeScale = 0;
    }

    private IEnumerator CheckWinGame()
    {
        while (true)
        {
            if (!CheckObjectsInList(ObjectLists.enemy))
            {
                Debug.Log("GAME WON");
                Time.timeScale = 0;
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
