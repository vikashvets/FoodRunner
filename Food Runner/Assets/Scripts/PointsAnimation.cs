using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointsAnimation : MonoBehaviour
{

    private float speed = 8f;
    private float TimeToDestroy = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveObject());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);//Movement of points up
    }
    IEnumerator RemoveObject()//Removes the animation of getting points after 0.5 second
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

}
