using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{

    [SerializeField] private GameObject shapePrefab;
    

    private void OnMouseDown()
    {
        PlayerController.Instance.Take();
        PlayerController.Instance.Give(shapePrefab);
    }
}
