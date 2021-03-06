﻿using UnityEngine;
using System.Collections;
using Utils;

public class ActivateChest : MonoBehaviour {

	public Transform lid, lidOpen, lidClose;	// Lid, Lid open rotation, Lid close rotation
	[SerializeField] private float openSpeed = 5F;				// Opening speed
	[SerializeField] private bool canClose;						// Can the chest be closed
	[SerializeField] private AudioSource lidNoise;
	
	private bool _open;											// Is the chest opened

	private bool _opening = false;

	// Rotate the lid to the requested rotation
	private IEnumerator ChestClicked()
	{
		lidNoise.Play();
		_opening = true;
		yield return Lerper.Lerp(lid, 5 / openSpeed, lid.position, _open ? lidClose.rotation : lidOpen.rotation);
		_open = !_open;
		_opening = false;
	}
	
	void OnMouseDown()
	{
		if (canClose && !_opening) StartCoroutine(ChestClicked());
	}
}
