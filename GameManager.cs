using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int currentGold;
    public Text goldText;
    public GameObject youWinText;
    public float resetDelay;

    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null){
            Destroy(gameObject);
        }
    }
    public void AddGold(int goldToAdd)
    {
        currentGold = currentGold + goldToAdd;
        goldText.text = "Gold: " + currentGold + "!"; // add on current gold to text
    }
    public void Win()
    {
        youWinText.SetActive(true); // display win message
        Time.timeScale = .5f;       // slow down time
        Invoke ("Reset", resetDelay); // call reset function after delay time
    }
    void Reset()
    {
        Time.timeScale = 1.0f; // set time to normal
        SceneManager.LoadScene(0); // load level 1 after winning
    }

}
