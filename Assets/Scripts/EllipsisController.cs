using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EllipsisController : MonoBehaviour
{
    RectMask2D rectMask;
    float[] paddingZValues = { 250, 125, 0, 125, 250, 375};

    void Awake()
    {
        rectMask = GetComponent<RectMask2D>();
    }

    void OnEnable()
    {
        StartCoroutine("DotDotDot");
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator DotDotDot()
    {
        while (true)
        {
            foreach (float zVal in paddingZValues)
            {
                rectMask.padding = new Vector4(0, 0, zVal, 0);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
