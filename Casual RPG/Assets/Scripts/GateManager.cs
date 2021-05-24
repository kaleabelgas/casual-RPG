using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private Sprite closedGate;
    [SerializeField] private Sprite openedGate;
    [SerializeField] private float timeToLock;

    private Collider2D gateCollider;
    private SpriteRenderer gateRenderer;
    private float _lockClock;

    private void Start()
    {
        gateCollider = gate.GetComponent<Collider2D>();
        gateRenderer = gate.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _lockClock -= Time.deltaTime;

        if (_lockClock >= 0) { CloseGate(); }
        else { OpenGate(); }
    }
    public void CloseGate()
    {
        gateCollider.enabled = true;
        gateRenderer.sprite = closedGate;
        _lockClock = timeToLock;
    }

    private void OpenGate()
    {
        gateCollider.enabled = false;
        gateRenderer.sprite = openedGate;
    }

}
