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
        switch(sceneName)
        {
            case "MmnVisual3DScene":
            case "MmnVisualAnimalScene":
                UnityEngine.VR.VRSettings.enabled = true;
                break;
            default:
                break;
        }
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
	}
}
