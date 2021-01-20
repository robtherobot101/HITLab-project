using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using Utils;

namespace Facilitator
{
    public class FacilitatorScript : MonoSingleton<FacilitatorScript>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject hat;
        [SerializeField] private TMP_Text textPrefab;
        [SerializeField] private Transform messageArea;
        [SerializeField] private GameObject nextButton;
        private IEnumerator<string> _messages;

        public void Hide()
        {
            canvas.enabled = false;
        }

        public void Say(string text)
        {
            var o = Instantiate(textPrefab);
            o.text = text;
            Say(o.gameObject);
        }

        public void Say(IEnumerable<string> messages)
        {
            _messages = messages.GetEnumerator();
            if (!_messages.MoveNext()) return;
            nextButton.SetActive(true);
            NextMessage();
        }

        public void NextMessage()
        {
            Say(_messages.Current);
            if (!_messages.MoveNext()) nextButton.SetActive(false);
        }

        public void Say(GameObject message)
        {
            messageArea.DestroyAllChildren();
            message.transform.SetParent(messageArea, false);
            canvas.enabled = true;
        }

        public IEnumerator Bounce()
        {
            var initialPos = hat.transform.localPosition;
            var initialRot = hat.transform.localRotation;
            yield return Lerper.Lerp(hat.transform, initialPos + 0.05f * Vector3.up,
                Quaternion.FromToRotation(Vector3.left, Vector3.up), 0.15f);
            yield return Lerper.Lerp(hat.transform, initialPos, initialRot, 0.15f);
        }
    }
}