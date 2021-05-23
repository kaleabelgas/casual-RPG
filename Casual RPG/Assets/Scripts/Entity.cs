using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntitySO thisEntity;

    protected HealthManager healthManager;

    protected virtual void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }
    protected virtual void Start()
    {
        gameObject.tag = thisEntity.Tag;

        healthManager.SetHealth(thisEntity.Health);
    }
}
