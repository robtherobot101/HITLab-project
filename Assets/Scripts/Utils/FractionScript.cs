using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

public class FractionScript : MonoBehaviour
{

    private TMP_Text numText;
    private TMP_Text denText;

    private Fraction _fraction;

    private void Start()
    {
        var o = GetComponentsInChildren<TMP_Text>();
        foreach (var p in o)
        {
            if (p.name.Equals("Numerator")) p.text = _fraction.Numerator.ToString();
            else if (p.name.Equals("Denominator")) p.text = _fraction.Denominator.ToString();
        }
    }

    public void SetFraction(Fraction fraction)
    {
        _fraction = fraction;
    }
}
