using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class BarrelScript : MonoBehaviour
{
    private GameObject shapePrefab;
    [SerializeField] private TMP_Text text;
    [SerializeField] private FractionScript fraction;
    [SerializeField] private HorizontalLayoutGroup fractionText;

    private void OnMouseDown()
    {
        if (PlayerController.Instance.Take("Shape") == null) PlayerController.Instance.Give(shapePrefab);
    }

    public void Init(GameObject shape, bool showFraction = false)
    {
        shapePrefab = shape;
        text.text = shape.GetComponent<ShapeScript>().ShapeName;
        fraction.SetFraction(shape.GetComponent<ShapeScript>().Fraction);
        if (!showFraction) Destroy(fractionText.gameObject);
    }
}