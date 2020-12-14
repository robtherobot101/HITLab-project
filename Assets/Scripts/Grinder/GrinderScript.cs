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
    private Fraction _fraction1 = Fraction.Zero, _fraction2 = Fraction.Zero;
    private FractionScript _fractionText1, _fractionText2;
    private bool _grinding = false;
    private bool _ground = false;
    private GameObject _grindingObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _powder = GetComponentInChildren<ParticleSystem>();
        _handle = GameObject.Find("Handle");
        _heap = Instantiate(heapPrefab, transform.position + Vector3.up * 1.49f, heapPrefab.transform.rotation, transform).GetComponent<HeapScript>();
        _fractionText1 = GameObject.Find("Fraction1").GetComponent<FractionScript>();
        _fractionText2 = GameObject.Find("Fraction2").GetComponent<FractionScript>();
        _fractionText1.enabled = false;
        _fractionText2.enabled = false;
        EventManager.Instance.missed += Clear;
        EventManager.Instance.reset += Clear;
    }

    private void OnMouseDown()
    {
        _grindingObject = PlayerController.Instance.Take();
        if (!_grinding && _grindingObject != null)
        {
            if (_fraction1 == Fraction.Zero)
            {
                _fraction1 = _grindingObject.GetComponent<ShapeScript>().Fraction;
                _fractionText1.SetFraction(_grindingObject.GetComponent<ShapeScript>().Fraction);
                _fractionText1.enabled = true;
            }
            else if (_fraction2 == Fraction.Zero)
            {
                _fraction2 = _grindingObject.GetComponent<ShapeScript>().Fraction;
                _fractionText2.SetFraction(_grindingObject.GetComponent<ShapeScript>().Fraction);
                _fractionText1.enabled = true;
            }
        }
    }

    private IEnumerator Grind()
    {
        _grinding = true;
        _powder.Play();
        StartCoroutine(_heap.Grow(_fraction1 + _fraction2));
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

    public void TurnHandle()
    {
        if (_fraction1 * _fraction2 != Fraction.Zero && !_ground)
        {
            StartCoroutine("Grind");
            _ground = true;
        }
    }
    
    private void Clear()
    {
        _ground = false;
        _fraction1 = Fraction.Zero;
        _fractionText1.SetFraction(Fraction.Zero);
        _fraction2 = Fraction.Zero;
        _fractionText2.SetFraction(Fraction.Zero);
        _fractionText1.enabled = false;
        _fractionText2.enabled = false;
    }
}
