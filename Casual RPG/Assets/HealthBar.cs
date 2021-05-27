using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;
    public void SetBarSize(float amount = 1)
    {
        bar.localScale = new Vector3(1f, amount);
    }
}
