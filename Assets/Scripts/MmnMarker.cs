using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

using Assets.LSL4Unity.Scripts;

public class MmnMarker : MonoBehaviour {

	public LSLMarkerStream markerStream;
	public float timeDuration = 0.5f;
	private float timeISI;
	public float timeSOA = 1.0f;
	public int numStimuli = 10;
	public float pDeviant = 0.2f;

	virtual protected void Start () {
		Assert.IsNotNull(markerStream, "You forgot to assign the reference to a marker stream implementation!");

		timeISI = timeSOA - timeDuration;
		Assert.IsTrue (timeISI > 0, "SOA must be longer than stimuls duration");
		Assert.IsTrue (pDeviant > 0 && pDeviant < 0.5, "deviant probability must be within 0 and 0.5");
	}

	virtual protected void Update () {
		if (Input.GetKeyUp(KeyCode.Space) && markerStream != null)
			StartCoroutine(WriteContinouslyMarkerEachSecond());
	}

	IEnumerator WriteContinouslyMarkerEachSecond()
	{
		int meanStandardsInRow = (int)((1.0f - pDeviant) / pDeviant);
		int minStandardsInRow = meanStandardsInRow / 2;
		int maxStandardsInRow = meanStandardsInRow * 3 / 2;
		int nextDeviant = 1 + UnityEngine.Random.Range (minStandardsInRow, maxStandardsInRow);

		for(int i = 0; i < numStimuli; i++)
		{
			if (i == nextDeviant) {
				markerStream.Write("Deviant");
				presentDeviant ();

				nextDeviant = i + 1 + UnityEngine.Random.Range (minStandardsInRow, maxStandardsInRow);
			}
			else {
				markerStream.Write ("Standard");
				presentStandard ();
			}
			yield return new WaitForSecondsRealtime(timeDuration);

			presentBreak();
			yield return new WaitForSecondsRealtime(timeISI);
		}
	}

	virtual protected void presentStandard()
	{
		Debug.Log ("Standard stimulus");
	}

	virtual protected void presentDeviant()
	{
		Debug.Log ("Deviant stimulus");
	}

	virtual protected void presentBreak()
	{
	}
}
