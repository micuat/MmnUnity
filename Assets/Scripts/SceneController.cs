using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UnityEngine.VR.VRSettings.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void SelectScene(string sceneName) {
		UnityEngine.VR.VRSettings.enabled = true;
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}
}
