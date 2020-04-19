using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject center;
    public GameObject explosionO;


    public float diff = 1f;

    float speedUpper = 30f;
    float speedLower = 5f;
    float maxSpeed = 0f;

    float spinTimer = 0.0f;
    float spinTimerMax = 0.0f;
    float spinTimerUpper = 9.0f;
    float spinTimerLower = 4.0f;

    float fallTimer = 0f;
    float fallTimerMax = 1f;

    int numFall = 0;

    float fallSpeed = 0.5f;
    float fallSpeedMax = 0.6f;
    int fallState = 0;

    int health = 1;

    bool canHarm = false;

    AudioSource explosion;

    string color;

    // Start is called before the first frame update
    void Start()
    {
        // findObjs();
        colorMe();
        firstSpin();
    }

    void colorMe()
    {
        int col = Random.Range(0, 3);


        if (col == 0)
        {
            color = "red";
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(255, 0, 0, 255);
        }

        else if (col == 1)
        {
            color = "blue";
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 255, 255);
        }

        else if (col == 2)
        {
            color = "yellow";
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 0, 255);
        }
    }




    void findObjs()
    {
        List<Component> aud = new List<Component>();
        AudioSource[] audio = GetComponents<AudioSource>();

        for (int i = 0; i < audio.Length; i++)
        {
            string st = audio[i].clip.ToString();

            if (st.Contains("Explosion"))
            {
                explosion = audio[i];
            }
        }
    }

    public void takeDamage(int amt, string pcolor)
    {

        if (canHarm && (pcolor == color))
        {
            health -= amt;
            if (health <= 0)
            {
                die();
            }
        }
    }


    
    void checkDist()
    {
        float dist = Vector3.Distance(center.transform.position, transform.position);


        if (!canHarm && dist < 8)
        {
            canHarm = true;
        }

        if (dist < 1.4)
        {
            center.GetComponent<Planet>().takeDamage(1);
            die();
        }

    }

    void explosionDo()
    {
        GameObject en = (GameObject)Instantiate(explosionO, transform.position, transform.rotation);

    }

    public void die()
    {
        explosionDo();



        center.GetComponent<Planet>().killAlien(1);
        center.GetComponent<Planet>().enemyDie(gameObject);       
        Destroy(gameObject);
    }

    void changeToFall()
    {
        fallState = 1;
        fallTimer = 0f;
        numFall += 1;
        fallSpeed = fallSpeedMax* diff;
        if (numFall == 1)
        {
            fallTimer = -0.8f;
            fallSpeed = 2f;
        }
    }

    void changeToSpin()
    {
        spinTimer = 0;
        fallState = 0;
        maxSpeed = Random.Range(speedLower, speedUpper) * diff;

        if (Random.Range(0,4) >= 2)
        {
            maxSpeed = maxSpeed * -1;
        }

        spinTimerMax = Random.Range(spinTimerLower, spinTimerUpper);
    }


    void updateTimerFall()
    {
        fallTimer += Time.deltaTime;
        if (fallTimer > fallTimerMax)
        {
            fallTimer = 0;
            changeToSpin();
        }
    }

    void updateTimerSpin()
    {
        spinTimer += Time.deltaTime;
        if (spinTimer > spinTimerMax)
        {
            spinTimer = 0;
            changeToFall();
        }
    }


    void firstSpin()
    {
        float s = Random.Range(0.0f, 360.0f); ;
        Vector3 c = center.transform.position;
        transform.RotateAround(c, Vector3.forward, s);
    }

    void moveSpin()
    {
        float s = -1 * Time.deltaTime * maxSpeed;
        Vector3 c = center.transform.position;
        transform.RotateAround(c, Vector3.forward, s);
    }

    void moveFall()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), center.transform.position, fallSpeed * Time.deltaTime);
    }


    void updateMovement()
    {

        if (fallState == 0)
        {
            moveSpin();
            updateTimerSpin();
        }
        else if (fallState == 1)
        {
            moveFall();
            updateTimerFall();
        }

    }


    // Update is called once per frame
    void Update()
    {
        checkDist();
    }

    void FixedUpdate()
    {
        updateMovement();
    }





}
