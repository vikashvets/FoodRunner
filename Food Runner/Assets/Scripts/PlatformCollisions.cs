using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollisions : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//Player collision handling
        {

            if (other is BoxCollider2D)//After a collision, the platform must go through the player
            {
                Transform platform = this.gameObject.transform;
                platform.GetComponent<PolygonCollider2D>().enabled = false;

            }
            else if (other is EdgeCollider2D) //After the collision, the player must stand on the platform
            {

                Transform platform = this.gameObject.transform;
                platform.GetComponent<PolygonCollider2D>().enabled = true;
            }

        }

    }
}
