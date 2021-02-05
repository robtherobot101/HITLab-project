using System;
using UnityEngine;
using Utils;

public class ChestScript : MonoBehaviour
{

    // [SerializeField] private Light keyholeLight;
    // [SerializeField] private Light glowLight;
    //
    // private float _keyholeLightIntensity;
    // private float _keyholeLightRange;
    //
    // private float _glowLightIntensity;
    // private float _glowLightRange;
    
    private void OnEnable()
    {
        var t = transform;
        var initialPosition = t.position;
        t.position = initialPosition + 10 * Vector3.down + 50 * Vector3.right;

        StartCoroutine(Lerper.Lerp(t, initialPosition, t.rotation, 3));
    }

    // private void Start()
    // {
    //     _keyholeLightIntensity = keyholeLight.intensity;
    //     _keyholeLightRange = keyholeLight.range;
    //
    //     _glowLightIntensity = glowLight.intensity;
    //     _glowLightRange = glowLight.range;
    // }

    // private void Update()
    // {
    //     keyholeLight.intensity = _keyholeLightIntensity + _keyholeLightIntensity * Mathf.Sin(2 * Time.time) / 4f;
    //     glowLight.intensity = _glowLightIntensity + _glowLightIntensity * Mathf.Sin(2 * Time.time) / 4f;
    // }
}