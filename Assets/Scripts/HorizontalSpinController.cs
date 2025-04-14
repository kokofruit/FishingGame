using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalSpinController : MonoBehaviour
{

    void OnEnable()
    {
        StartCoroutine("SpinModel");
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpinModel()
    {
        transform.rotation = Quaternion.identity;
        while (true)
        {
            transform.Rotate(new Vector3(0f, 36 * Time.deltaTime, 0f));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
