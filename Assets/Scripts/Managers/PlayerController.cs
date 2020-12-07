using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

public class PlayerController : MonoSingleton<PlayerController>
{
    private GameObject _holding;
    
    public void Give([CanBeNull] GameObject o)
    {
        if (_holding != null) Destroy(_holding);
        _holding = Instantiate(o);
    }
    
    [CanBeNull]
    public GameObject Take()
    {
        Destroy(_holding);
        return _holding;
    }
}
