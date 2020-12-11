using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.WSA;
using Utils;

public class PlayerController : MonoSingleton<PlayerController>
{
    private GameObject _holding;
    private const float HoldDistance = 2f;
    private const float SpinFrequency = 0.5f;
    private Camera _camera;
    private bool _isHolding = false;

    
    public void Give([CanBeNull] GameObject o)
    {
        if (_holding != null) Destroy(_holding);
        _holding = Instantiate(o);
        _holding.layer = 2;
        _isHolding = true;
    }
    
    [CanBeNull]
    public GameObject Take()
    {
        Destroy(_holding);
        _isHolding = false;
        return _holding;
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_isHolding)
        {
            _holding.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition + HoldDistance * Vector3.forward);
            _holding.transform.Rotate(Vector3.up, Time.deltaTime * 360 * SpinFrequency);
        }
    }
}
