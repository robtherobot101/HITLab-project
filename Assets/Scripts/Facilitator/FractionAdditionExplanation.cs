using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

namespace Facilitator
{
    public class FractionAdditionExplanation : MonoBehaviour
    {
        [SerializeField] private FractionScript fraction1Text;
        [SerializeField] private FractionScript fraction2Text;
        [SerializeField] private FractionScript solution;

        [SerializeField] private TMP_Text arrow1;
        [SerializeField] private TMP_Text arrow2;
        [SerializeField] private TMP_Text arrow3;

        private bool _buttonPressed;
        private Fraction _fraction1;
        private Fraction _fraction2;

        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(nameof(NextInstruction));
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void Init(Fraction fraction1, Fraction fraction2)
        {
            _fraction1 = fraction1;
            _fraction2 = fraction2;

            fraction1Text.SetFraction(fraction1);
            fraction2Text.SetFraction(fraction2);
        }

        public void Next()
        {
            _buttonPressed = true;
        }

        private IEnumerator NextInstruction()
        {
            var solutionNumerator = solution.NumText;
            var solutionDenominator = solution.DenText;

            solutionNumerator.text = " ";
            solutionDenominator.text = " ";

            yield return new WaitUntil(() => _buttonPressed);
            // Multiply the first's numerator by the second's denominator
            arrow1.enabled = true;
            _buttonPressed = false;
            yield return new WaitUntil(() => _buttonPressed);
            solutionNumerator.text = (_fraction1.Numerator * _fraction2.Denominator).ToString();
            _buttonPressed = false;

            yield return new WaitUntil(() => _buttonPressed);
            // Multiply the seconds's numerator by the first's denominator
            arrow1.enabled = false;
            arrow2.enabled = true;
            _buttonPressed = false;
            yield return new WaitUntil(() => _buttonPressed);
            solutionNumerator.text += "+" + _fraction2.Numerator * _fraction1.Denominator;
            _buttonPressed = false;

            yield return new WaitUntil(() => _buttonPressed);
            // Multiply the first's numerator by the second's denominator
            arrow2.enabled = false;
            arrow3.enabled = true;
            _buttonPressed = false;
            yield return new WaitUntil(() => _buttonPressed);
            solutionDenominator.text = (_fraction1 + _fraction2).Denominator.ToString();
        }
    }
}