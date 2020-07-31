using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOvertime : MonoBehaviour {

    public float lifetime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, lifetime); // Destroy object when lifetime runs out
	}
}
// After a certain amount of time, the object will be removed from world
