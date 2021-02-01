using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{

    [SerializeField] private AudioSource sound;
    private float _goalVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        _goalVolume = sound.volume;
        sound.volume = 0f;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (sound.volume < _goalVolume)
        {
            sound.volume = Mathf.Lerp(0, _goalVolume, Time.time / 20f);
            yield return null;
        }
    }
    
}
