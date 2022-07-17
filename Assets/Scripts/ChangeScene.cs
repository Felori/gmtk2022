using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public int sceneToLoad;
	public KeyCode changeSceneKey;
	public KeyCode resetKey;

	void Update () {
		if (Input.GetKeyDown(resetKey))
			ResetScene();
		if (Input.GetKeyDown(changeSceneKey))
			LoadScene();
	}

	public void LoadScene() {
		SceneManager.LoadScene(sceneToLoad);
	}

	public void ResetScene () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
