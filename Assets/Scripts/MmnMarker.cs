using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

using Assets.LSL4Unity.Scripts;

public class MmnMarker : MonoBehaviour {

	public GameObject[] stimulusObjects;
	public LSLMarkerStream markerStream;
	public float visibleDuration = 1.0f;
	public float invisibleDuration = 1.0f;

	void Start () {
		Assert.IsNotNull(markerStream, "You forgot to assign the reference to a marker stream implementation!");

		if (markerStream != null)
			StartCoroutine(WriteContinouslyMarkerEachSecond());

		Assert.IsFalse (stimulusObjects.Length < 2, "Not enough stimulus objects are defined");

		foreach (var obj in stimulusObjects) {
			obj.GetComponent<Renderer> ().enabled = false;
		}
	}
	
	IEnumerator WriteContinouslyMarkerEachSecond()
	{
		while (true)
		{
			var currentIndex = GetARandomMarker();
			markerStream.Write(stimulusObjects[currentIndex].name);
			stimulusObjects[currentIndex].GetComponent<Renderer> ().enabled = true;
			yield return new WaitForSecondsRealtime(visibleDuration);
			foreach (var obj in stimulusObjects) {
				obj.GetComponent<Renderer> ().enabled = false;
			}
			yield return new WaitForSecondsRealtime(invisibleDuration);
		}
	}

	private int GetARandomMarker()
	{
		return UnityEngine.Random.value > 0.8 ? 0 : 1;
	}
}
