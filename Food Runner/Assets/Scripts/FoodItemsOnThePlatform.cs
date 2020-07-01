using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItemsOnThePlatform : MonoBehaviour
{
    public GameObject[] FoodItemsOnThePlatformArray;
    private Vector3 NewPosition;
    private LifeController GameLifeController;
    // Start is called before the first frame update
    void Start()
    {
        GameLifeController = FindObjectOfType<LifeController>();
        if (!GameLifeController.IsSetted)//If game is not over
        {
            //  Create a random food item on the new platform
            NewPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            int rand = Random.Range(0, FoodItemsOnThePlatformArray.Length);
            Instantiate(FoodItemsOnThePlatformArray[rand], NewPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
