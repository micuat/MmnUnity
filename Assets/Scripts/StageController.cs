using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {
    public bool setVr = false;

	// Use this for initialization
	void Start () {
        UnityEngine.VR.VRSettings.enabled = setVr;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
