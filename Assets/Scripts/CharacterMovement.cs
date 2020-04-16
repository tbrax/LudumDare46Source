using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    public Canvas scoreBoard;
    public Transform scoreText;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public GameObject bullet;
    public GameObject enemy;
    Quaternion point;
    // Start is called before the first frame update
    float enemyTimer;
    float enemyTimerMax;
    float fireTimer;
    float fireTimerMax;
    int scoreT;
    int numLemons;

  

    public List<GameObject> touchListLemon = new List<GameObject>();

    public List<GameObject> touchListTree = new List<GameObject>();

    void Start()
    {
        numLemons = 0;

        fireTimer = 0f;

        fireTimerMax = 0.1f;
        scoreT = numLemons;
        updateScore();
    }


    public void updateScore()
    {
        scoreT = numLemons;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: "+ scoreT.ToString();
    }
    Vector2 randomEnemyPos()
    {
        Vector2 p = new Vector2(0, 0);
        int side = Random.Range(0, 4);
        float rangee = 4.0f;

        if (side == 0)
        {
            p.x = Random.Range(-rangee, rangee);
            p.y = 7;
        }
        else if (side == 1)
        {
            p.x = 7;
            p.y = Random.Range(-rangee, rangee);
        }
        else  if (side == 2)
        {
            p.x = Random.Range(-rangee, rangee);
            p.y = -7;
        }
        else
        {
            p.x = -7;
            p.y = Random.Range(-rangee, rangee);
        }

        return p;
    }



    void checkBounds()
    {
        float xLimit = 5.5f;
        float yLimit = 4f;


        if (rb.position.x > xLimit)
        {
            rb.position = new Vector2(xLimit, rb.position.y);
        }
        else if (rb.position.x < -xLimit)
        {
            rb.position = new Vector2(-xLimit, rb.position.y);
        }

        if (rb.position.y > yLimit)
        {
            rb.position = new Vector2(rb.position.x, yLimit);
        }
        else if (rb.position.y < -yLimit)
        {
            rb.position = new Vector2(rb.position.x, -yLimit);
        }
    }
    public void takeDamage()
    {
        numLemons = 0;
        scoreT = 0;
        updateScore();
    }


    void updateFire()
    {
        fireTimer += Time.deltaTime;
    }


    Vector2 getAim()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        Vector3 dir = (pz - this.transform.position).normalized;

        return new Vector2(dir.x,dir.y);
    }




    void doPick()
    {
       
        for (var i = 0; i < touchListLemon.Count; i++)
        {
            numLemons += 1;
            updateScore();
            touchListLemon[i].GetComponent<Lemon>().die();

        }
    }

    void doWater()
    {
        for (var i = 0; i < touchListTree.Count; i++)
        {
            touchListTree[i].GetComponent<Tree>().water();
        }
    }

    void keyWater()
    {

        doWater();
    }

    void keyPick()
    {
        doPick();

    }

    void keyControls()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {
            keyPick();
        }



    }

    // Update is called once per frame
    void Update()
    {

        keyControls();
        updateFire();

        checkBounds();
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");



    }
    

    private void FixedUpdate()
    {
 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "lemon")
        {
            touchListLemon.Add(collision.gameObject);
        }

        else if (collision.gameObject.tag == "tree")
        {
            touchListTree.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "lemon")
        {
            touchListLemon.Remove(collision.gameObject);

        }

        else if (collision.gameObject.tag == "tree")
        {
            touchListTree.Remove(collision.gameObject);
        }
    }

}
