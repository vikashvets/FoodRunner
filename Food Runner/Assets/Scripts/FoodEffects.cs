using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEffects : MonoBehaviour
{
    private int counter = 0;
    private float speed = 0.8f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //script animation for food, its can be replaced by animator
        if (counter < 10)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);//change position 
            counter++;
        }
        else if (counter < 20 && counter >= 10)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            counter++;
        }
        if (counter >= 20)
        {
            counter = 0;
        }
    }
}
