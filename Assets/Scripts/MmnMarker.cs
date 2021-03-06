﻿using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

using Assets.LSL4Unity.Scripts;
using System.Collections.Generic;

public class MmnMarker : MonoBehaviour {

	public LSLMarkerStream markerStream;
	public float timeDuration = 0.5f;
	private float timeISI;
	public float timeSOA = 1.0f;
	public float timeDelayToStart = 1.0f;
	public int[] numStimuli = { 10, 10 };
	public float pDeviant = 0.2f;
	public double timeLSLRecordingOffset = 0;
    int currentTrial = 0;

	private bool isTriggering = false;

	virtual protected void Start () {
		Assert.IsNotNull(markerStream, "You forgot to assign the reference to a marker stream implementation!");

		timeISI = timeSOA - timeDuration;
		Assert.IsTrue (timeISI > 0, "SOA must be longer than stimuls duration");
		Assert.IsTrue (pDeviant > 0 && pDeviant < 0.5, "deviant probability must be within 0 and 0.5");
	}

	virtual protected void Update () {
		if (isTriggering == false && markerStream != null && currentTrial < numStimuli.Length) {
			if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
				|| Input.GetKeyUp (KeyCode.Space)) {
				StartCoroutine (WriteContinouslyMarkerEachSecond ());
				isTriggering = true;
			}
		}
	}

	IEnumerator WriteContinouslyMarkerEachSecond()
	{
		int meanStandardsInRow = Mathf.RoundToInt((1.0f - pDeviant) / pDeviant);
		int minStandardsInRow = Mathf.RoundToInt(meanStandardsInRow / 2.0f);
		int maxStandardsInRow = Mathf.RoundToInt(meanStandardsInRow * 3.0f / 2.0f);
		int nextDeviant = 1 + UnityEngine.Random.Range (minStandardsInRow, maxStandardsInRow + 1);

        yield return new WaitForSecondsRealtime(timeDelayToStart);

		for(int i = 0; i < numStimuli[currentTrial]; i++)
		{
			if (i == nextDeviant) {
                writeMarker("Deviant");
				presentDeviant ();

				nextDeviant = i + 1 + UnityEngine.Random.Range (minStandardsInRow, maxStandardsInRow + 1);
			}
			else {
                writeMarker("Standard");
				presentStandard ();
			}
			yield return new WaitForSecondsRealtime(timeDuration);

			presentBreak();
			yield return new WaitForSecondsRealtime(timeISI);
		}

		isTriggering = false;
        currentTrial += 1;
	}

    virtual protected void writeMarker(string marker)
    {
        markerStream.Write(marker, LSL.liblsl.local_clock() + timeLSLRecordingOffset);
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
