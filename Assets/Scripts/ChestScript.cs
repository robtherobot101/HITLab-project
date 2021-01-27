using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ChestScript : MonoBehaviour
{
    private void OnEnable()
    {
        var initialPosition = transform.localPosition;
        transform.localPosition = initialPosition + 10 * Vector3.down + 50 * Vector3.right;

        StartCoroutine(Lerper.Lerp(transform, initialPosition, transform.rotation, 3));
    }
}
