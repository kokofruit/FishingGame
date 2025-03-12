using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] MeshFilter model;
    [SerializeField] Mesh modelToUse;

    private void Start()
    {
        model.mesh = modelToUse;
    }
}
