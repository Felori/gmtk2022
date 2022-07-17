using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] TMP_Text healthNumber;

    public void SetHealth(int health)
    {
        healthNumber.text = Mathf.Max(health, 0).ToString();
    }
}
