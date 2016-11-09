using UnityEngine;
using System.Collections;
using LSL;

public class LslFramerateOutlet : MonoBehaviour
{
    float deltaTime = 0.0f;

    private liblsl.StreamOutlet outlet;
    private liblsl.StreamInfo streamInfo;
    private float[] currentSample;

    public string StreamName = "UnityStream";
    public string StreamType = "FPS";
    public int ChannelCount = 1;

    // Use this for initialization
    void Start()
    {
        currentSample = new float[ChannelCount];

        streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, Time.fixedDeltaTime * 1000);
        outlet = new liblsl.StreamOutlet(streamInfo);
    }

    // http://wiki.unity3d.com/index.php?title=FramesPerSecond
    public void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        currentSample[0] = fps;

        outlet.push_sample(currentSample);
    }

}
