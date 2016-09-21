using UnityEngine;
using System.Collections;

public class MmnShapeMarker : MmnMarker {

	public GameObject standardObject;
	public GameObject deviantObject;

	// Use this for initialization
	override protected void Start () {
		standardObject.GetComponent<Renderer> ().enabled = false;
		deviantObject.GetComponent<Renderer> ().enabled = false;

		base.Start ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		base.Update ();
	}

	override protected void presentStandard()
	{
		standardObject.GetComponent<Renderer> ().enabled = true;
	}

	override protected void presentDeviant()
	{
		deviantObject.GetComponent<Renderer> ().enabled = true;
	}

	override protected void presentBreak()
	{
		standardObject.GetComponent<Renderer> ().enabled = false;
		deviantObject.GetComponent<Renderer> ().enabled = false;
	}
}
