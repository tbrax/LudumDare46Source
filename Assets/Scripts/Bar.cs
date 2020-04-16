using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

    public List<Sprite> levels = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void updateLevel(float min, float max)
    {
        float per = (float)min / (float)max;

        int i = (int)((levels.Count-1)* per);
        this.GetComponent<SpriteRenderer>().sprite = levels[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
