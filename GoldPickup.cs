using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour {

    public int value;

    public GameObject pickupEffect;

	void Start () {
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)             // Whenever another object enters trigger area
    {
        if (other.tag == "Player")                           // If player collides
        {
            
            FindObjectOfType<GameManager>().AddGold(value); // Find gamemanager object and run addgold function
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);                            // Gold disappears
        }
    }

}
