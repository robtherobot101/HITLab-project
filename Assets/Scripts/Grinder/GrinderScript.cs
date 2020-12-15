using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grinder;
using Managers;
using UnityEditor.Analytics;
using UnityEngine;
using Utils;

public class GrinderScript : MonoBehaviour
{

    private GameObject _handle;
    private ParticleSystem _powder;
    private HeapScript _heap;
    
    private bool _grinding = false;
    private bool _ground = false;

    public IEnumerable<ShapeScript> Shapes => _shapes;
    private ISet<ShapeScript> _shapes = new HashSet<ShapeScript>();
    
    // Start is called before the first frame update
    private void Start()
    {
        _powder = GetComponentInChildren<ParticleSystem>();
        _handle = GameObject.Find("Handle");
        _heap = GetComponentInChildren<HeapScript>();

        // EventManager.Instance.missed += Clear;
        EventManager.Instance.reset += Clear;
    }

    private void OnMouseDown()
    {
        var grindingObject = PlayerController.Instance.Take();
        if (!_grinding && GameManager.Instance.CurrentGoal.CanAdd())
        {
            _shapes.Add(grindingObject.GetComponent<ShapeScript>());
            ScreenManager.Instance.Write(grindingObject.GetComponent<ShapeScript>().Fraction);
            ScreenManager.Instance.Write("text");
        }
    }

    private IEnumerator Grind()
    {
        _grinding = true;
        _powder.Play();
        StartCoroutine(_heap.Grow(_shapes.Aggregate(Fraction.Zero, (current, shape) => current + shape.Fraction))); 
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
        if (!_ground && GameManager.Instance.CurrentGoal.RequirementsSatisfied())
        {
            StartCoroutine("Grind");
            _ground = true;
        }
    }
    
    private void Clear()
    {
        _ground = false;
        _shapes.Clear();
        ScreenManager.Instance.Clear();
    }
}
