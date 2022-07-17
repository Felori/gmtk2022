using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int healthEffect = 0;
    [SerializeField] int temperatureEffect = 0;

    public void Apply(GameManager manager)
    {
        manager.ChangePlayerHealth(healthEffect);
        manager.ChangeFoodTemperature(temperatureEffect);
    }

    IEnumerator PickupAnimation()
    {
        float time = 0f;
        while(time < 1f)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            transform.Rotate(Vector3.up, Time.deltaTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    private void Awake()
    {
        StartCoroutine(PickupAnimation());
    }
}
