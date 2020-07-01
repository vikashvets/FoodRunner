using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    private float speed = 6;
    private float TimeToDestroy = 9;
    private bool IsTimeToComplicate = true;
    private LifeController GameLifeController;

    // Start is called before the first frame update
    void Start()
    {
        GameLifeController = FindObjectOfType<LifeController>();
        //Permanent existing object for speed control
        if (gameObject.tag != "moving_anchor")
        {
            Invoke("RemoveObject", TimeToDestroy);
        }


    }

    // Update is called once per frame
    void Update()
    {//moving left each second
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (IsTimeToComplicate)
        // The complexity of the game over time
        {
            StartCoroutine(TimeDecrease());
            IsTimeToComplicate = false;
        }

    }

    public void RemoveObject()
    {
        // Removing an object after it disappears from the scene
        if (!GameLifeController.IsSetted)
        // If the game is not finished, delete the object
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
    public void StopMoving()
    {
        speed = 0f;
    }

    public IEnumerator TimeDecrease()//Speed ​​increase
    {
        yield return new WaitForSeconds(55);
        if (speed < 11)//Restrictions so that the movement is not too fast
        {
            speed += 1f;
            TimeToDestroy -= 1.5f;
        }
        IsTimeToComplicate = true;

    }
}
