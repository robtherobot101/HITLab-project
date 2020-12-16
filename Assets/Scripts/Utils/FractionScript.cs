using TMPro;
using UnityEngine;
using Utils;

public class FractionScript : MonoBehaviour
{
    private Fraction _fraction;
    private TMP_Text denText;

    private TMP_Text numText;

    private void Start()
    {
        var o = GetComponentsInChildren<TMP_Text>();
        foreach (var p in o)
            if (p.name.Equals("Numerator")) numText = p;
            else if (p.name.Equals("Denominator")) denText = p;
        SetFraction(_fraction);
    }

    public void SetFraction(Fraction fraction)
    {
        SetFraction(fraction, Color.black);
    }

    public void SetFraction(Fraction fraction, Color colour)
    {
        _fraction = fraction;
        if (numText != null)
        {
            numText.text = fraction.Numerator.ToString();
            denText.text = fraction.Denominator.ToString();
        }

        foreach (var child in GetComponentsInChildren<TMP_Text>()) child.color = colour;
    }
}