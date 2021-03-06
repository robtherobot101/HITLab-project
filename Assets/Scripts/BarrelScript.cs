﻿using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private FractionScript fraction;
    [SerializeField] private HorizontalLayoutGroup fractionText;
    [SerializeField] private GameObject label;
    private GameObject _shapePrefab;

    private void OnMouseDown()
    {
        // Either take the shape held by the player, or give them the contents of the barrel
        var o = PlayerController.Instance.Take("Shape");
        if (o == null)
            PlayerController.Instance.Give(_shapePrefab);
        else
            Destroy(o);
    }

    public void Init(ShapeScript shape, bool showFraction = false, bool showLabel = true)
    {
        _shapePrefab = shape.gameObject;
        text.text = shape.ShapeName;
        fraction.SetFraction(shape.Fraction);
        if (!showFraction) Destroy(fractionText.gameObject);
        if (!showLabel) Destroy(label);
    }
}