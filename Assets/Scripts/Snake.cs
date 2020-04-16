using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private float fTurnRate = 300.0f;  // 90 degrees of turning per second
    private float fSpeed = 2.0f;  // Units per second of movement;
    Vector2 movement;

    public List<Vector2> posList = new List<Vector2>();
    public List<float> rotList = new List<float>();
    float posTimer = 0.0f;
    float posUpd = 0.01f;

    float fruitTimer = 0.0f;
    float fruitUpd = 5f;


    public Rigidbody2D rb;

    public int addSize = 5;
    public int tailSize = 0;
    public GameObject tailObj;

    public GameObject fruit;


    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> bodyList = new List<GameObject>();

    public Transform scoreText;
    int scoreT;
    // Start is called before the first frame update
    void Start()
    {
        updateScore(0);
        checkTailL();

        scoreT = 0;
        //addTail();
        //addTail();
        //addTail();

    }

    void removeTail()
    {
        
        for (var i = 0; i < bodyList.Count; i++)
        {
            //int id = bodyList.Count - i - 1;
            //GameObject temp = bodyList[i];
            //bodyList.Remove(temp);
            Destroy(bodyList[i]);
        }

        bodyList.Clear();

    }

    void die()
    {
        scoreT = 0;
        updateScore(0);
        removeTail();
        transform.position = new Vector2(0, 0);
        tailSize = 0;
    }


    public void updateScore(int amt)
    {
        scoreT += amt;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + scoreT.ToString();
    }


    Vector2 randomFruitPos()
    {
        return new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
    }

    public void addFruit()
    {
        GameObject en = (GameObject)Instantiate(fruit, randomFruitPos(), transform.rotation);
        en.GetComponent<Fruit>().head = gameObject;
    }
    public void eatFruit()
    {
        updateScore(1);
        addTail();
    }

    Vector2 lastTailPos()
    {
        if (bodyList.Count == 0)
        {
            return transform.position;
        }

        int c = bodyList.Count - 1;
        return new Vector2(bodyList[c].transform.position.x, bodyList[c].transform.position.y);
    }

    void checkTailL()
    {
        if (posList.Count < (tailSize+2) * addSize)
        {
            for (var i = 0; i < addSize; i++)
            {
                posList.Add(new Vector2(transform.position.x, transform.position.y));
                rotList.Add(transform.eulerAngles.z);
            }
        }
        
    }


    void addTail()
    {
        tailSize += 1;
        checkTailL();


     
        GameObject en = (GameObject)Instantiate(tailObj, lastTailPos(), transform.rotation);
        en.GetComponent<SnakeBody>().head = gameObject;
        en.GetComponent<SnakeBody>().place = tailSize;
        bodyList.Add(en);
    }

    void checkBounds()
    {
        float xLimit = 5.3f;
        float yLimit = 4.2f;


        if (transform.position.x > xLimit)
        {
            transform.position = new Vector2(xLimit, transform.position.y);
            die();
        }
        else if (transform.position.x < -xLimit)
        {
            transform.position = new Vector2(-xLimit, transform.position.y);
            die();
        }

        if (transform.position.y > yLimit)
        {
            transform.position = new Vector2(transform.position.x, yLimit);
            die();
        }
        else if (transform.position.y < -yLimit)
        {
            transform.position = new Vector2(transform.position.x, -yLimit);
            die();
        }
    }


    void updateTail()
    {
        int c = posList.Count;
        for (var i = 0; i < c-1; i++)
        {
            posList[c-i-1] = posList[c-i-2];
            rotList[c - i - 1] = rotList[c - i - 2];
        }
        posList[0] = new Vector2(transform.position.x, transform.position.y);
        rotList[0] = transform.rotation.eulerAngles.z;
        //print(posList[1]);


    }

    void checkFruit()
    {
        fruitTimer += Time.deltaTime;
        if (fruitTimer >= fruitUpd)
        {
            fruitTimer = 0;
            addFruit();
        }

    }

    void checkTail()
    {
        posTimer += Time.deltaTime;
        if (posTimer > posUpd)
        {
            updateTail();
            posTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "body")
        {
            die();
        }
    }



    void Update()
    {
        checkBounds();
        checkTail();
        checkFruit();


            movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        transform.Rotate(-Vector3.forward * fTurnRate *movement.x *Time.deltaTime);

        transform.localPosition = transform.localPosition + transform.up * fSpeed * Time.deltaTime;
    }
}
