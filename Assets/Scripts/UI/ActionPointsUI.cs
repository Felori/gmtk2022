using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionPointsUI : MonoBehaviour
{
    [SerializeField] TMP_Text[] actionPointNumbers = default;

    public void SetActionPoints(int[] actionPoints)
    {
        for(int i = 0; i < Mathf.Min(actionPointNumbers.Length, actionPoints.Length); i++) {
            actionPointNumbers[i].text = actionPoints[i].ToString();
        }
    }

    public void SetCurrentActionPoints(int points)
    {
        actionPointNumbers[0].text = points.ToString();
    }
}
