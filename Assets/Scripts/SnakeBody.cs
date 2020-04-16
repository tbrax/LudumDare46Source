using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{

    public GameObject head;
    Vector2 pos;
    public int place;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void remove()
    {
        head.GetComponent<Snake>().bodyList.Remove(gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        int index = (place * head.GetComponent<Snake>().addSize)-1;
        Vector2 pos = head.GetComponent<Snake>().posList[index];
        float rot = head.GetComponent<Snake>().rotList[index];
        transform.position = pos;
        transform.eulerAngles = new Vector3(0,0,rot);
    }
}
