using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponentInParent<GrinderScript>().TurnHandle();
    }
}
