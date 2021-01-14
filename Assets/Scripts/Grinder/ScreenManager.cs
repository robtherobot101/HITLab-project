using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

namespace Grinder
{
    public class ScreenManager : MonoSingleton<ScreenManager>
    {

        private GameObject _screen;
        [SerializeField] private GameObject correct;
        [SerializeField] private GameObject incorrect;

        private void Start()
        {
            correct.SetActive(false);
            incorrect.SetActive(false);
        }

        public GameObject SetScreen(GameObject screen)
        {
            Destroy(_screen);
            _screen = Instantiate(screen, transform);
            return _screen;
        }

        public void FlashResult(bool isCorrect)
        {
            var o = isCorrect ? correct : incorrect;
            StartCoroutine(Flash(o));
        }

        private IEnumerator Flash(GameObject o)
        {
            o.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            o.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            o.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            o.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
        
    }
}