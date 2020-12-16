using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

public class FacilitatorScript : MonoSingleton<FacilitatorScript>
{
    private Canvas _canvas;
    private GameObject _hat;
    private TMP_Text _text;

    // Start is called before the first frame update
    private void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _text = _canvas.GetComponentInChildren<TMP_Text>();
        _hat = GameObject.Find("Hat");
        Hide();
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }

    public void Say(string text)
    {
        _text.text = text;
        _canvas.enabled = true;
    }

    public IEnumerator Bounce()
    {
        var initialPos = _hat.transform.position;
        var initialRot = _hat.transform.rotation;
        yield return Lerper.Lerp(_hat.transform, initialPos + Vector3.up,
            Quaternion.FromToRotation(Vector3.left, Vector3.up), 0.15f);
        yield return Lerper.Lerp(_hat.transform, initialPos, initialRot, 0.15f);
    }
}