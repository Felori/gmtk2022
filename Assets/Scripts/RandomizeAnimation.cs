using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimation : MonoBehaviour {
	void Start() {
		StartCoroutine(RandomTrigger("Attack", 6));
		StartCoroutine(RandomTrigger("Take Damage", 10));
		StartCoroutine(RandomTrigger("Die", 20));
	}

	IEnumerator RandomTrigger(string triggerName, float time) {
		yield return new WaitForSeconds(Random.Range(0, time));
		GetComponent<Animator>().SetTrigger(triggerName);
		StartCoroutine(RandomTrigger(triggerName, time));
	}
}
