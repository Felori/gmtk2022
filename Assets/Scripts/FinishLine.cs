using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour, ICharacterEnterHandler
{
    public static event Action onPlayerEnteredFinishLine;
    public void OnCharacterEnter(Character character)
    {
        if(character is Player)
        {
            onPlayerEnteredFinishLine?.Invoke();
        }
    }
}
