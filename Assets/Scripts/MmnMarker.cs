using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

using Assets.LSL4Unity.Scripts;

public class MmnMarker : MonoBehaviour {

	public GameObject[] stimulusObjects;
	public LSLMarkerStream markerStream;
	public float timeDuration = 0.5f;
	private float timeISI;
	public float timeSOA = 1.0f;
	public int numStimuli;
	public float pDeviant;

	void Start () {
		Assert.IsNotNull(markerStream, "You forgot to assign the reference to a marker stream implementation!");

		Assert.IsFalse (stimulusObjects.Length < 2, "Not enough stimulus objects are defined");

		foreach (var obj in stimulusObjects) {
			obj.GetComponent<Renderer> ().enabled = false;
		}

		timeISI = timeSOA - timeDuration;
		Assert.IsTrue (timeISI > 0, "SOA must be longer than stimuls duration");
		Assert.IsTrue (pDeviant > 0 && pDeviant < 0.5, "deviant probability must be within 0 and 0.5");
	}

	void Update () {
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

	void presentStandard()
	{
		stimulusObjects[0].GetComponent<Renderer> ().enabled = true;
	}

	void presentDeviant()
	{
		stimulusObjects[1].GetComponent<Renderer> ().enabled = true;
	}

	void presentBreak()
	{
		foreach (var obj in stimulusObjects) {
			obj.GetComponent<Renderer> ().enabled = false;
		}
	}
}
