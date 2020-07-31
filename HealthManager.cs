using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    public float invincibilityLength;               // So that they don't take double damage when bumped into another obstacle by accident
    public float invincibilityCounter;

    private bool isRespawning;
    private Vector3 respawnPoint;                  // Position where they respawn
    public float respawnLength;

    public GameObject deathEffect;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

	void Start () {
        currentHealth = maxHealth; // Full health at start of game

        //thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position; // Respawn point is wherever the player is as soon as game starts
	}
	
	
	void Update () {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
		
	}
    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)               // If invin counter is below 0, THEN we can take more damage
        {
            currentHealth -= damage;                 // Take away damage value from health

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                thePlayer.Knockback(direction);          // Knockback player

                invincibilityCounter = invincibilityLength;
            }
        }

        if (isFadeToBlack)
        {
            // Fade to black
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        if (isFadeFromBlack)
        {
            // No more fading to black
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }


    }
    public void Respawn()
    {
        if (!isRespawning)                              // IF we're not already respawning
        {
            StartCoroutine("RespawnCo");                // Starting a coroutine
        }
    }
    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);          // Player gone from world
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;
        
        yield return new WaitForSeconds(waitForFade); // Wait for however long the respawn length is

        isFadeFromBlack = true;

        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;    // Respawn
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;
    }


    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
