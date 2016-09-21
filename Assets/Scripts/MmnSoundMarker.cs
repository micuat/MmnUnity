using UnityEngine;
using System.Collections;

public class MmnSoundMarker : MmnMarker {

	public AudioSource wavStandard;
	public AudioSource wavDeviant;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
	}

	// Update is called once per frame
	override protected void Update () {
		base.Update ();
	}

	override protected void presentStandard()
	{
		wavStandard.Play ();
	}

	override protected void presentDeviant()
	{
		wavDeviant.Play ();
	}
}
