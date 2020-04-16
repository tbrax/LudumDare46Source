using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : MonoBehaviour
{

    float lifeTimer;
    float maxLifeTimer = 3;
    public GameObject bar;

    GameObject myBar;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = maxLifeTimer;
        makeBar();
    }

    void makeBar()
    {
        Vector3 newPos = transform.position + new Vector3(0,1,0);
        GameObject en = (GameObject)Instantiate(bar, newPos, transform.rotation);
        myBar = en;
    }

    void updateBar()
    {
        myBar.GetComponent<Bar>().updateLevel(lifeTimer, maxLifeTimer);
    }

    public void die()
    {
        Destroy(myBar);
        Destroy(gameObject);
    }

    void checkLife()
    {
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <=0)
        {
            die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateBar();
        checkLife();
    }
}
