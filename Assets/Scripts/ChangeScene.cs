using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public int sceneToLoad;

	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
			ResetScene();
		if (Input.GetKeyDown(KeyCode.Space))
			LoadScene();
	}

	public void LoadScene() {
		SceneManager.LoadScene(sceneToLoad);
	}

	public void ResetScene () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
