using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Grinder
{
    public class ScreenManager : MonoSingleton<ScreenManager>
    {
        public void SetScreen(GameObject screen)
        {
            Instantiate(screen, transform);
        }
    }
}
