using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive = 1;
    AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
		
	}
	
	
	void Update () {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            Vector3 hitDirection = other.transform.position - transform.position;     // PLAYER GETS HIT
            
            hitDirection = hitDirection.normalized;                                   // Restricts to be within a straight line distance, consistent knockbacks
            
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection); // Goes to healthmanager and tells damage...
            audioSource.Play();
        }
       else
        {
           audioSource.Stop();
        }
    }
}
