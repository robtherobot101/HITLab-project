using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Grinder
{
    public class ScreenManager : MonoSingleton<ScreenManager>
    {
        public GameObject SetScreen(GameObject screen)
        {
            return Instantiate(screen, transform);
        }
    }
}
