using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GrinderScript : MonoBehaviour
{

    [SerializeField] private GameObject heapPrefab;
    private GameObject _handle;
    private ParticleSystem _powder;
    private HeapScript _heap;
    private bool _grinding;
    private GameObject _grindingObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _powder = GetComponentInChildren<ParticleSystem>();
        _handle = GameObject.Find("Handle");
        _heap = Instantiate(heapPrefab, transform.position + Vector3.up * 1.49f, heapPrefab.transform.rotation, transform).GetComponent<HeapScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        _grindingObject = PlayerController.Instance.Take();
        if (!_grinding && _grindingObject != null)
        {
            StartCoroutine("Grind");
        }
    }

    private IEnumerator Grind()
    {
        _grinding = true;
        _powder.Play();
        StartCoroutine(_heap.Grow(_grindingObject.GetComponent<ShapeScript>().Fraction));
        float t = 0;
        while (t < 4)
        {
            _handle.transform.Rotate(0, -1, 0);
            t += Time.deltaTime;
            yield return null;
        }
        _powder.Stop();
        _grinding = false;
    }
}
