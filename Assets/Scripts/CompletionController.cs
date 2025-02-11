using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionController : MonoBehaviour
{
    public static CompletionController instance;
    [SerializeField] float gainSpeed;
    [SerializeField] float loseSpeed;
    SpriteMask mask;
    bool isGaining = false;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mask = GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGaining)
        {
            mask.alphaCutoff -= gainSpeed * Time.deltaTime;
            if (mask.alphaCutoff <= 0)
            {
                print("gain fish");
            }
        }
        else
        {
            mask.alphaCutoff += loseSpeed * Time.deltaTime;
            if (mask.alphaCutoff >= 1)
            {
                print("lose fish");
            }
        }
    }

    public void SetGaining(bool gainStatus)
    {
        isGaining = gainStatus;
    }
}
