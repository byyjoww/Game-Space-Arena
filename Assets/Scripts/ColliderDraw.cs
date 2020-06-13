using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderDraw : MonoBehaviour
{
    private Vector2 size;

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        size = RectTransformUtility.CalculateRelativeRectTransformBounds(transform as RectTransform).size;
        gameObject.GetComponent<BoxCollider2D>().size = size;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
    }
}
