using UnityEngine;
using System.Collections;

public class MmnShapeMarker : MmnMarker {

	public GameObject standardObject;
	public GameObject deviantObject;

	void SetVisibility(GameObject g, bool visible) {
		var r = g.GetComponent<Renderer>();
		if(r != null) {
			r.enabled = visible;
		}
		foreach (Transform c in g.transform)
		{
			SetVisibility(c.gameObject, visible);
		}
	}

	// Use this for initialization
	override protected void Start () {
		SetVisibility(standardObject, false);
		SetVisibility(deviantObject, false);

		base.Start ();
	}

	// Update is called once per frame
	override protected void Update () {
		base.Update ();
	}

	override protected void presentStandard()
	{
		SetVisibility(standardObject, true);
	}

	override protected void presentDeviant()
	{
		SetVisibility(deviantObject, true);
	}

	override protected void presentBreak()
	{
		SetVisibility(standardObject, false);
		SetVisibility(deviantObject, false);
	}
}
