using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScript : MonoBehaviour
{
    private GrinderScript _grinderScript;

    private void Start()
    {
        _grinderScript = GetComponentInParent<GrinderScript>();
    }
    private void OnMouseDown()
    {
        _grinderScript.TurnHandle();
    }
}
