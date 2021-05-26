using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private IMovement movement;
    [SerializeField] private WeaponHolder weaponHolder;

    [SerializeField] private PlayerHealthManager playerHealthManager;

    [SerializeField] private Transform padLocation;

    [SerializeField] private float respawnTime;

    [SerializeField] private Renderer playerRenderer;

    [SerializeField] private KeyboardInput keyboardInput;
    public float RespawnClock { get; private set; }

    protected override void Awake()
    {
        movement = GetComponent<IMovement>();
    }
    protected override void Start()
    {
        playerHealthManager.SetHealth(thisEntity.Health);
        movement.SetMoveSpeed(thisEntity.MoveSpeed);
        playerHealthManager.OnPlayerDeath += TeleportToPad;
        GameManager.Instance.AddObjectToList(GameManager.ObjectLists.player, gameObject);
    }

    private void TeleportToPad()
    {
        playerRenderer.enabled = false;
        keyboardInput.DisableInput();
        transform.position = padLocation.position;
        RespawnClock = respawnTime;
        Debug.Log("Teleported");
    }

    private void Update()
    {
        RespawnClock -= Time.deltaTime;
        //Debug.Log($"Time until Spawn {(int)RespawnClock}");

        if (RespawnClock <= 0) { Respawn(); } 

        if(RespawnClock > 0) { return; }


        if (Input.GetKeyDown(KeyCode.E)) { weaponHolder.Equip(); }
        if (Input.GetKeyDown(KeyCode.Q)) { weaponHolder.DropCurrentWeapon(); }
        if (Input.GetKeyDown(KeyCode.R)) { weaponHolder.GiveWeaponToTower(); }
        if (Input.GetKey(KeyCode.Space)) { weaponHolder.UseWeapon(); }
    }

    private void Respawn()
    {
        if (playerHealthManager.CurrentPlayerHealth > 0) { return; }

        keyboardInput.EnableInput();
        playerHealthManager.ResetHealth();
        playerRenderer.enabled = true;

    }
}
