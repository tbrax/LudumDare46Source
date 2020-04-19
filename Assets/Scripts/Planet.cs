using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Planet : MonoBehaviour
{
    // Start is called before the first frame update

    int planetHealth;
    int maxPlanetHealth = 3;
    float enemySpawnTimer = 0;
    float enemySpawnTimerMax = 1;
    int maxEnemiesMax = 10;
    int maxEnemies = 10;
    public GameObject enemy;
    public Transform scoreText;
    public GameObject piece;
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> truckList = new List<GameObject>();
    int score;
    float diff = 1f;
    float diffTimer = 0f;
    float diffTimerMax = 15f;
    bool dead = false;
    float deadAnimTimer = 0.0f;
    float deadAnimTimerMax = 5.0f;
    public GameObject cracks;
    public List<Sprite> crackSprites = new List<Sprite>();
    public List<AudioClip> audioTreadList = new List<AudioClip>();
    public AudioSource soundPlayer;
    float treadSoundTimer = 0.0f;
    float treadSoundTimerMax = 0.2f;

    void Start()
    {
        resetMap();
        updateScore();
    }

    void updateCracks()
    {
        int s = (maxPlanetHealth - planetHealth);

        if (crackSprites.Count -1 < s)
        {
            s = crackSprites.Count - 1;
        }

        cracks.GetComponent<SpriteRenderer>().sprite = crackSprites[s];
    }

    void updateDiff()
    {
        updateScore();
        maxEnemies = Mathf.RoundToInt(maxEnemiesMax * diff);
    }

    void goToTitle()
    {
        SceneManager.LoadScene("title");
    }


    void checkDead()
    {
        if (dead)
        {
            deadAnimTimer += Time.deltaTime;
            if (deadAnimTimer > deadAnimTimerMax)
            {
                goToTitle();
            }         
        }
    }


    void spawnPlanetPieces()
    {
        float bulletOffset = 0.13f;

        for (var i = 0; i < 6; i++)
        {
            Quaternion q = Quaternion.Euler(0, 0, Random.Range(0, 360));
            GameObject en = (GameObject)Instantiate(piece, transform.position + transform.up * bulletOffset, q);
        }
    }

    void killEnemy()
    {
        for (var i = 1; i < enemyList.Count; i++)
        {
            enemyList[enemyList.Count-i].GetComponent<Alien>().die();
        }


    }

    void lose()
    {

        killEnemy();
        for (var i = 0; i < truckList.Count; i++)
        {
            GameObject t = truckList[i];

            t.GetComponent<Truck>().die();
        }

        
        cracks.GetComponent<Renderer>().enabled = false;

        gameObject.GetComponent<Renderer>().enabled = false;
        dead = true;
        spawnPlanetPieces();


    }

    public void killAlien(int sc)
    {
        score += sc;
        updateScore();
    }

    public void updateScore()
    {
        int hp = planetHealth;
        int sc = score;

        int d = Mathf.RoundToInt((((diff - 1) * 10) + 1));

        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Danger Level: " + d.ToString() + "\n" +
            "Score: " + sc.ToString();
    }


    public void enemyDie(GameObject en)
    {
        enemyList.Remove(en);
    }

    void resetMap()
    {
        dead = false;
        diffTimer = 0;
        planetHealth = maxPlanetHealth;
        enemySpawnTimer = 0;
        diff = 1f;
        updateDiff();
        updateCracks();
    }

    public void takeDamage(int amt)
    {
        if (!dead)
        {
            planetHealth -= amt;

            if (planetHealth <= 0)
            {
                planetHealth = 0;
                lose();
            }
            updateCracks();
            updateScore();
        }


    }
    void spawnEnemy()
    {
        if (!dead && enemyList.Count < maxEnemies)
        {
            float upwards = 8.7f;
            GameObject en = (GameObject)Instantiate(enemy, transform.position + transform.up * upwards, transform.rotation);
            en.GetComponent<Alien>().center = gameObject;

            en.GetComponent<Alien>().diff = diff;
            enemyList.Add(en);
        }
    }

    void updateDifficulty()
    {
        diffTimer += Time.deltaTime;
        if (diffTimer > diffTimerMax )
        {
            diffTimer = 0;
            diff += 0.1f;
        }
    }


    void updateTimer()
    {

        if (!dead)
        {
            updateControls();
            updateDifficulty();
        }

        checkDead();

        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer > enemySpawnTimerMax)
        {
            enemySpawnTimer = 0;
            spawnEnemy();
        }
    }


    void treadSound()
    {
        // 
        int i = Random.Range(0, audioTreadList.Count);
        soundPlayer.clip = audioTreadList[i];
        soundPlayer.Play();
    }

    void updateControls()
    {
        treadSoundTimer += Time.deltaTime;


        float x = Input.GetAxis("Horizontal");
        if (Mathf.Abs(x) > 0.0)
        {
            if (treadSoundTimer > treadSoundTimerMax)
            {
                treadSoundTimer = 0;

                treadSound();
            }
        }
        else
        {
            soundPlayer.Stop();
        }
        


    }


    // Update is called once per frame
    void Update()
    {
        updateTimer();
        
    }
}
