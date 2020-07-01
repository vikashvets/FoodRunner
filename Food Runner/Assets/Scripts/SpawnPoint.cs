using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] Platforms;
    public GameObject[] PlatformsWithoutFood;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreateItem()
    {// The random definition of the object that will be created.
        int rand = Random.Range(0, items.Length);
        if (items[rand].tag == "trap")
        {
            int chance = Random.Range(0, 2);//The chance of a trap is 1/2
            if (chance == 1)// If a trap should appear, a platform without food will spawn above it so that the player can avoid a collision with the trap.
            {
                if (PlatformsWithoutFood.Length != 0)
                {
                    var RandPlatform = Random.Range(0, Platforms.Length);
                    Instantiate(PlatformsWithoutFood[RandPlatform], new Vector3(transform.position.x, -0.5f, transform.position.z), Quaternion.identity);
                }

                Instantiate(items[rand], transform.position, Quaternion.identity);


            }
        }
        else //trap shouldnt apear
        {
            Instantiate(items[rand], transform.position, Quaternion.identity);

        }

    }



}
