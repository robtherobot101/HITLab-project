using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utils;

public class InstructionsManager : MonoSingleton<InstructionsManager>
{

    private TMP_Text _tmp;
    private Transform _cameraTransform;
    
    protected override void Init()
    {
        _tmp = GetComponentInChildren<TMP_Text>();
        _cameraTransform = Camera.main.transform;
    }

    public void SetText(string text)
    {
        _tmp.text = text;
    }

    private void Update()
    {
        // var position = _cameraTransform.position +
        //                2 * (-_cameraTransform.up - new Vector3(0, 2 * _cameraTransform.forward.y, 0));
        // transform.SetPositionAndRotation(position, _cameraTransform.rotation);
    }
}
