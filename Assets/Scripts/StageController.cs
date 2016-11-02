using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {
    public bool setVr = false;
    private TouchScript.Gestures.FlickGesture gesture;
    private SceneController sceneController;
    public GameObject environmentObject;

    // Use this for initialization
    void Start () {
        UnityEngine.VR.VRSettings.enabled = setVr;

        gesture = GetComponent<TouchScript.Gestures.FlickGesture>();
        gesture.Flicked += flickedHandler;

        sceneController = SceneController.InstanceRef;
        if(sceneController.Is3DEnv)
        {
            if (environmentObject != null)
            {
                environmentObject.SetActive(true);
            }
        }
        else
        {
            if (environmentObject != null)
            {
                environmentObject.SetActive(false);
            }
            Camera.main.clearFlags = CameraClearFlags.Color;
            Camera.main.backgroundColor = Color.black;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount == 2 || Input.GetKeyUp(KeyCode.Escape))
        {
            if (setVr)
            {
                UnityEngine.VR.VRSettings.enabled = false;
            }
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main");
        }
    }

    private void flickedHandler(object sender, System.EventArgs e)
    {
        if (setVr)
        {
            UnityEngine.VR.VRSettings.enabled = false;
        }
        Debug.Log("flicked");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main");
    }
}
