using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float lifeTimer = 0;
    float lifeTimerMax = 10f;

    float speed = 1.0f;

    public Vector3 movement;

    public string color = "white";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void die()
    {
        Destroy(gameObject);
    }

    void updateLife()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTimerMax)
        {
            die();
        }
    }

    void Update()
    {
        updateLife();
    }

    private void FixedUpdate()
    {
        transform.position += movement * Time.deltaTime * speed;
    }


    void dealDamage(GameObject enemy)
    {
        enemy.GetComponent<Alien>().takeDamage(1, color);
        die();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            dealDamage(collision.gameObject);
        }

    }



}
