using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    private float TimeBtwSpawn;
    public float StartTimeBtwSpawn;
    private bool IsTimeToComplicate = true;
    // Start is called before the first frame update
    void Start()
    {
        //spawn points instantiate
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Instantiate(SpawnPoints[i], SpawnPoints[i].transform.position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {// Counts the time between spawn.If it has already passed, spawn a new platform using a spawn point.
        if (TimeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, SpawnPoints.Length);
            SpawnPoints[rand].GetComponent<SpawnPoint>().CreateItem();
            TimeBtwSpawn = StartTimeBtwSpawn;
        }
        else
        {
            TimeBtwSpawn -= Time.deltaTime;
        }

        if (IsTimeToComplicate)
        {
            IsTimeToComplicate = false;
            StartCoroutine(TimeDecrease());
        }


    }

    public IEnumerator TimeDecrease()//Decrease in time between spawn.Platforms begin to spawn more often.
    {
        yield return new WaitForSeconds(55);
        IsTimeToComplicate = true;
        if (StartTimeBtwSpawn > 2f && gameObject.name != "TutorialPlatformSpawner")//Doesn't decrease the time for spawn platforms in the tutorial
        {
            StartTimeBtwSpawn -= 0.3f;
        }

    }

}
