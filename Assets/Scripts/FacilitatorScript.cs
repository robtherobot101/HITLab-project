using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FacilitatorScript : MonoSingleton<FacilitatorScript>
{

    private Canvas _canvas;
    private TMP_Text _text;
    
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _text = _canvas.GetComponentInChildren<TMP_Text>();
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
}
