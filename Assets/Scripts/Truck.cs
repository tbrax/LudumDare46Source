using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
using UnityEditor;

public class Truck : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 movement;
    public float treadMaxSpeed = 50.0f;

    public string color = "white";

    public float bulletSpeed = 10f;
    float fall = 0.08f;
    float jumpAmt = -10000f;
    public GameObject center;
    Vector3 gravity;
    float treadTimer = 0;
    float treadTimerMax = 0.2f;
    int treadState = 0;
    float fireTimer = 0;
    float fireTimerMax = 0.4f;
    public List<Sprite> treadSprites = new List<Sprite>();
    public GameObject bullet;

    bool dead = false;
    AudioSource soundFire;


    GameObject treadObject;
    // Start is called before the first frame update
    void Start()
    {
        findObjs();

    }

    void findObjs()
    {
        treadObject = gameObject.transform.Find("treadsbig").gameObject;
        findAudio();
    }

    void findAudio()
    {
        List<Component> aud = new List<Component>();
        AudioSource[] audio = GetComponents<AudioSource>();

        for (int i = 0; i < audio.Length; i++)
        {
            string st = audio[i].clip.ToString();

            if (st.Contains("Fire"))
            {
                soundFire = audio[i];
            }
        }
    }




    void colorBullet(GameObject b)
    {
        if (color == "red")
        {
            b.GetComponent<SpriteRenderer>().color = new Vector4(255, 0, 0, 255);
        }

        else if (color == "blue")
        {
            b.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 255, 255);
        }

        else if (color == "yellow")
        {
            b.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 0, 255);
        }
    }

    void spawnBullet()
    {
        float bulletOffset = 0.13f;
        GameObject en = (GameObject)Instantiate(bullet, transform.position + transform.up* bulletOffset, transform.rotation);

        Vector3 v = transform.up * bulletSpeed;
        en.GetComponent<Bullet>().movement = v;
        en.GetComponent<Bullet>().color = color;
        colorBullet(en);
;
    }

    void shoot()
    {
        if (fireTimer > fireTimerMax)
        {
            fireTimer = 0;
            spawnBullet();

            if (soundFire)
            {
                soundFire.Play();
            }

        }

    

    }

    public void die()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        dead = true;

    }


    void swapTread()
    {
        if (treadState == 0)
        {
            treadState = 1;
            treadObject.GetComponent<SpriteRenderer>().sprite = treadSprites[0];


        }
        else if (treadState == 1)
        {
            treadState = 0;
            treadObject.GetComponent<SpriteRenderer>().sprite = treadSprites[1];
        }
    }
    void treadMove()
    {
        treadTimer += Time.deltaTime;
        if (treadTimer > treadTimerMax)
        {
            treadTimer = 0;
            swapTread();
        }
    }

    void updateMovement()
    {
        
        float s = -1 * Time.deltaTime * treadMaxSpeed * movement.x;
        if (Math.Abs(movement.x) > 0)
        {
            treadMove();
        }

        Vector3 c = center.transform.position;
        transform.RotateAround(c, Vector3.forward, s);

    }


    void updateTimers()
    {
        fireTimer += Time.deltaTime;
    }

    void updateControls()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void updateKeys()
    {
        if (Input.GetButtonDown("Jump"))
        {
            shoot();
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            updateKeys();
            updateControls();
            updateMovement();
            updateTimers();
        }
    }


    void tread()
    {
        //rb.MovePosition(transform.position + gravity + transform.right * Time.fixedDeltaTime * treadSpeed * movement.x);
    }

    void jump()
    {
        Vector3 c = center.transform.position;
        Vector3 dir = (c - gameObject.transform.position).normalized;
        gravity = dir * jumpAmt;
        rb.AddForce(gravity);
    }


    void applyGravity()
    {
        Vector3 c = center.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(c);

        //transform.LookAt(c);
        Vector3 dir = (c - gameObject.transform.position).normalized;
        gravity = dir * fall;
        rb.AddForce(gravity * Time.deltaTime);
    }
    void FixedUpdate()
    {
        
        updateMovement();
    }

}

