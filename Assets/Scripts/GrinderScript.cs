using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderScript : MonoBehaviour
{

    [SerializeField] private GameObject heapPrefab;
    private ParticleSystem _powder;
    
    // Start is called before the first frame update
    void Start()
    {
        _powder = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (PlayerController.Instance.Take() != null)
        {
            StartCoroutine("Grind");
        }
    }

    private IEnumerator Grind()
    {
        _powder.Play();
        Instantiate(heapPrefab, transform.position + Vector3.up * 1.49f, heapPrefab.transform.rotation, transform);
        yield return new WaitForSeconds(4);
        _powder.Stop();
    }
}
