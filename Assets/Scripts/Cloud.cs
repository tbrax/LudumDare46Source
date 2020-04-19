using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public float speed;
    public GameObject center;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void updateMovement()
    {

        float s = -1 * Time.deltaTime * speed;


        Vector3 c = center.transform.position;
        transform.RotateAround(c, Vector3.forward, s);

    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
    }
}
