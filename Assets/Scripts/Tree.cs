using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    float totalTimer;
    float lifeTimer;
    float lifeTimerMax = 10;

    float lemonTimer;
    float lemonTimerMax = 5;
    public GameObject lemon;

    // Start is called before the first frame update
    void Start()
    {
        lemonTimer = Random.Range(0, 3);
    }

    public void water()
    {
        lifeTimer = lifeTimerMax;
    }

    void makeLemon()
    {
        Vector3 newPos = transform.position + new Vector3(0, -1, 0);

        GameObject en = (GameObject)Instantiate(lemon, newPos, transform.rotation);

    }

    void checkLemon()
    {
        lemonTimer += Time.deltaTime;
        if (lemonTimer > lemonTimerMax)
        {
            makeLemon();
            lemonTimer = 0 - Random.Range(0, 3);
        }
    }

    void checkLife()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        checkLife();
        checkLemon();
        totalTimer += Time.deltaTime;
        

    }
}
