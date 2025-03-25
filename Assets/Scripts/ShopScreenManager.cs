using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManager : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ButtonOperation);
    }

    public void ButtonOperation(){
        print("dolla dolla bills");
    }
}
