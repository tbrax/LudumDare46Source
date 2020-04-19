using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    float lifeTimer = 0;
    float lifeTimerMax = 10f;
    float speed = 2.0f;
    public Vector3 movement;
    float rotateSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        movement = transform.right;
    }

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

    // Update is called once per frame
    void Update()
    {
        updateLife();
        moveRot();
    }


    void moveRot()
    {
        transform.position += movement * Time.deltaTime * speed;
        transform.Rotate(0.0f, 0.0f, rotateSpeed);
    }


}
