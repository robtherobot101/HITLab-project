using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Grow");
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(1);
        float timePassed = 0;
        while (timePassed < 4)
        {
            transform.localScale += Vector3.up * (Time.deltaTime * 0.4f);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
