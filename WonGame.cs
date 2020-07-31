using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonGame : MonoBehaviour {
    
    void OnTriggerEnter()
    {
        GameManager.instance.Win();
    }

}
