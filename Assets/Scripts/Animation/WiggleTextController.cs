using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleTextController : MonoBehaviour
{
    RectTransform rect;
    List<RectTransform> childRectList = new();
    [SerializeField] float wiggleDistance;
    [SerializeField] float wiggleSpeed;
    [SerializeField] float wiggleOffset;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        foreach (Transform childTransform in rect)
        {
            childRectList.Add(childTransform.GetComponent<RectTransform>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < childRectList.Count; i++)
        {
            RectTransform childRect = childRectList[i];
            childRect.localPosition = new Vector2(childRectList[i].localPosition.x, Mathf.Sin((Time.time + (i / wiggleOffset)) * wiggleSpeed) * wiggleDistance);
        }
    }
}
