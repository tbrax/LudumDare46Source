using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    float lifeTimer = 0;
    float lifeTimerMax = 1.0f;

    float animTimer = 0.0f;
    float animTimerMax = 0.5f;

    public List<Sprite> sprites = new List<Sprite>();
    int spriteInd = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void die()
    {
        Destroy(gameObject);
    }


    void updateAnim()
    {
        animTimer += Time.deltaTime;

        float aStep = animTimerMax / (sprites.Count+1);

        //int s = Mathf.RoundToInt(aStep);

      

        if (animTimer >= aStep)
        {
            
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (animTimer >= animTimerMax)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }

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
        updateAnim();
    }
}
