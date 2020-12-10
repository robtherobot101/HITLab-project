using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class HeapScript : MonoBehaviour
{
    public IEnumerator Grow(Fraction fraction)
    {
        yield return new WaitForSeconds(0.8f);
        float timePassed = 0;
        while (timePassed < 4)
        {
            transform.localScale += Vector3.up * (Time.deltaTime * fraction.Value() / 2);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}
