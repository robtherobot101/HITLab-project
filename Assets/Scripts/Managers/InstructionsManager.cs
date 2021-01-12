using TMPro;
using UnityEngine;
using Utils;

namespace Managers
{
    public class InstructionsManager : MonoSingleton<InstructionsManager>
    {
        [SerializeField] private TMP_Text tmp;

        public void SetText(string text)
        {
            tmp.text = text;
        }
    }
}