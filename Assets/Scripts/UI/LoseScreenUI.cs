using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreenUI : MonoBehaviour
{
    public void ShowScreen()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        StartCoroutine(ShowDelayed());
    }

    IEnumerator ShowDelayed()
    {
        yield return new WaitForSeconds(3f);
        transform.localScale = Vector3.one;
    }
}
