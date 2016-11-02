using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour {

    private bool _Is3DEnv;
    public bool Is3DEnv
    {
        get { return instanceRef._Is3DEnv; }
        set { instanceRef._Is3DEnv = value; }
    }

    private static SceneController instanceRef;
    public static SceneController InstanceRef
    {
        get { return instanceRef; }
    }

    public Toggle guiEnv;

    void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
            Is3DEnv = false;
        }
        else
        {
            // don't kill me... GUI requires this object
            //DestroyImmediate(gameObject);
        }

        UnityEngine.VR.VRSettings.enabled = false;
    }

    // Use this for initialization
    void Start () {
        guiEnv.isOn = Is3DEnv;
	}

	// Update is called once per frame
	void Update () {

	}

	public void SelectScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
	}
}
