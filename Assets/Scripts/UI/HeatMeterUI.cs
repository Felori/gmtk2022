using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMeterUI : MonoBehaviour
{
    [SerializeField] Animator animator = default;

    public void SetValue(float value)
    {
        animator.SetFloat("Heat", value);
    }
}
