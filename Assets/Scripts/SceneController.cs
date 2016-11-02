using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour {

    private bool is3DEnv;
    public bool Is3DEnv
    {
        get { return instanceRef.is3DEnv; }
        set { instanceRef.is3DEnv = value; }
    }

    private static SceneController instanceRef;
    public static SceneController InstanceRef
    {
        get { return instanceRef; }
    }

    private Toggle guiToggle3DEnv;
    public Toggle GuiToggle3DEnv
    {
        get { return guiToggle3DEnv; }
        set
        {
            guiToggle3DEnv = value;
            if (guiToggle3DEnv != null)
            {
                guiToggle3DEnv.isOn = Is3DEnv;
            }
        }
    }

    void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);

            Is3DEnv = false;
            InstanceRef.GuiToggle3DEnv = GameObject.Find("Toggle3DEnv").GetComponent<Toggle>();
        }
        else
        {
            InstanceRef.GuiToggle3DEnv = GameObject.Find("Toggle3DEnv").GetComponent<Toggle>();

            DestroyImmediate(gameObject);
        }

        UnityEngine.VR.VRSettings.enabled = false;
    }

    // Use this for initialization
    void Start () {

    }

	// Update is called once per frame
	void Update () {

	}

	public void SelectScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
	}
}
