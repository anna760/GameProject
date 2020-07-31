using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public HealthManager theHealthMan;

    public Renderer theRend;

    public Material cpOff;
    public Material cpOn;

    

	void Start () {
        theHealthMan = FindObjectOfType<HealthManager>();
	}
	
	
	void Update () {
		
	}

    public void CheckpointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>(); // array of checkpoints
        foreach(Checkpoint cp in checkpoints)                       // as long as there are more checkpoints in world, it'll be off until one is on
        {
            cp.CheckpointOff();
        }

        theRend.material = cpOn;
    }

    public void CheckpointOff()
    {
        theRend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            theHealthMan.SetSpawnPoint(transform.position); // New spawn point
            CheckpointOn();
        }
    }
}
