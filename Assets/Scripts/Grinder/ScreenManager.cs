using UnityEngine;
using Utils;

namespace Grinder
{
    public class ScreenManager : MonoSingleton<ScreenManager>
    {
        public GameObject SetScreen(GameObject screen)
        {
            foreach (Transform t in transform) Destroy(t.gameObject);
            return Instantiate(screen, transform);
        }
    }
}