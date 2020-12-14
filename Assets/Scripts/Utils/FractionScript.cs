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
            if (p.name.Equals("Numerator")) numText = p;
            else if (p.name.Equals("Denominator")) denText = p;
        }
        SetFraction(_fraction);
    }

    public void SetFraction(Fraction fraction)
    {
        _fraction = fraction;
        if (numText != null)
        {
            numText.text = fraction.Numerator.ToString();
            denText.text = fraction.Denominator.ToString();
        }
    }
}
