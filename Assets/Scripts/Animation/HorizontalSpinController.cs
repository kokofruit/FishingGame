using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// animation code for spinning an object horizontally from right to left
public class HorizontalSpinController : MonoBehaviour
{

    void OnEnable()
    {
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        // rotate the transform of the object
        transform.Rotate(new Vector3(0f, 36 * Time.deltaTime, 0f));
    }
}
