using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Grinder
{
    public class ScreenManager : MonoSingleton<ScreenManager>
    {

        [SerializeField] private GameObject fractionPrefab;
        [SerializeField] private GameObject textPrefab;
        private Transform _currentLine;
        
        // Start is called before the first frame update
        void Start()
        {
            _currentLine = transform.GetChild(0);
        }

        public void Write(Fraction fraction)
        {
            var f = Instantiate(fractionPrefab, _currentLine);
            
            f.GetComponent<FractionScript>().SetFraction(fraction);
        }

        public void Write(string text)
        {
            var t = Instantiate(textPrefab, _currentLine);
            t.GetComponent<TMP_Text>().text = text;
        }

        public void Clear()
        {
            foreach (Transform child in _currentLine.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
