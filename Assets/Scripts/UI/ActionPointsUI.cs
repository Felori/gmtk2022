using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionPointsUI : MonoBehaviour
{
    [SerializeField] TMP_Text[] actionPointNumbers = default;
    [SerializeField] Animator animator = default;

    public void SetActionPoints(int[] actionPoints, bool playAnimation)
    {
        for(int i = 0; i < Mathf.Min(actionPointNumbers.Length, actionPoints.Length); i++) {
            actionPointNumbers[i].text = actionPoints[i].ToString();
        }

        if (playAnimation) animator.SetTrigger("NewNumpers");
    }

    public void SetCurrentActionPoints(int points)
    {
        actionPointNumbers[0].text = points.ToString();
    }
}
