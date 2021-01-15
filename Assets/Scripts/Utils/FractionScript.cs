using TMPro;
using UnityEngine;

namespace Utils
{
    public class FractionScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text numText;
        [SerializeField] private TMP_Text denText;
        [SerializeField] private TMP_Text vinculum;
        private Fraction _fraction;

        public TMP_Text NumText => numText;
        public TMP_Text DenText => denText;

        private void OnEnable()
        {
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

            numText.color = colour;
            denText.color = colour;
            vinculum.color = colour;
        }
    }
}