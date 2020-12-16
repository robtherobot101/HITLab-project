using TMPro;
using UnityEngine;

namespace Utils
{
    public class FractionScript : MonoBehaviour
    {
        private Fraction _fraction;
        private TMP_Text _denText;

        private TMP_Text _numText;

        private void Start()
        {
            var o = GetComponentsInChildren<TMP_Text>();
            foreach (var p in o)
                switch (p.name)
                {
                    case "Numerator":
                        _numText = p;
                        break;
                    case "Denominator":
                        _denText = p;
                        break;
                }
            SetFraction(_fraction);
        }

        public void SetFraction(Fraction fraction)
        {
            SetFraction(fraction, Color.black);
        }

        public void SetFraction(Fraction fraction, Color colour)
        {
            _fraction = fraction;
            if (_numText != null)
            {
                _numText.text = fraction.Numerator.ToString();
                _denText.text = fraction.Denominator.ToString();
            }

            foreach (var child in GetComponentsInChildren<TMP_Text>()) child.color = colour;
        }
    }
}