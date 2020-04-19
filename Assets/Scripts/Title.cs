using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour
{

    public GameObject credits;
    public GameObject title;

    bool creds = true;
    // Start is called before the first frame update
    void Start()
    {
        toggleCredits();
    }

    void startGame()
    {
        SceneManager.LoadScene("game");
    }

    void toggleCredits()
    {
        if (creds)
        {
            creds = false;
            title.SetActive(true);
            credits.SetActive(false);
        }
        else
        {
            creds = true;
            title.SetActive(false);
            credits.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            startGame();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            toggleCredits();
        }
    }
}
