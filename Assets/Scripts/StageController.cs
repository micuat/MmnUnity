using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {
    public bool setVr = false;
    private TouchScript.Gestures.FlickGesture gesture;

    // Use this for initialization
    void Start () {
        UnityEngine.VR.VRSettings.enabled = setVr;

        gesture = GetComponent<TouchScript.Gestures.FlickGesture>();
        gesture.Flicked += flickedHandler;
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
