using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class Entity : MonoBehaviour
{
    [SerializeField] protected EntitySO thisEntity;

    private HealthManager healthManager;

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
