using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class BarrelScript : MonoBehaviour
{

    [SerializeField] private GameObject shapePrefab;
    
    private void OnMouseDown()
    {
        PlayerController.Instance.Take("Shape");
        PlayerController.Instance.Give(shapePrefab);
    }

    public static GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, GameObject shape)
    {
        var o = Instantiate(prefab, position, rotation);
        o.GetComponent<BarrelScript>().shapePrefab = shape;
        o.GetComponentInChildren<TMP_Text>().text = shape.GetComponent<ShapeScript>().ShapeName;
        o.GetComponentInChildren<FractionScript>().SetFraction(shape.GetComponent<ShapeScript>().Fraction);
        return o;
    }
}


