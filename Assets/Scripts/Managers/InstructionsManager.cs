using TMPro;
using UnityEngine;
using Utils;

namespace Managers
{
    public class InstructionsManager : MonoSingleton<InstructionsManager>
    {
        private TMP_Text _tmp;

        protected override void Init()
        {
            _tmp = GetComponentInChildren<TMP_Text>();
        }

        public void SetText(string text)
        {
            _tmp.text = text;
        }
    }
}