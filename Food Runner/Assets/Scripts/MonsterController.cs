using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Vector3 newPosition;
    private Rigidbody2D MonsterRigidbody;
    private Animator playerAnimation;
    private Animator monsterAnimation;
    private PlayerController GamePlayerController;
    private GameObject [] Spawners;
    private MovingController [] MovingControllers;
    

    // Start is called before the first frame update
    void Start()
    {
        MonsterRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Monster movement after losing or gaining life
    public void MonsterMove()
    {
       MonsterRigidbody.AddForce(transform.right * 1000);
       Invoke("MonsterStop", 1);
       
    }
    public void MonsterMoveBack()
    {
        MonsterRigidbody.AddForce(transform.right * 1000* -1);
        Invoke("MonsterStopBack", 1);

    }

    public void MonsterStop()
    {

     MonsterRigidbody.AddForce(transform.right * 1000 * -1);

    }

    public void MonsterStopBack()
    {
        MonsterRigidbody.AddForce(transform.right * 1000);
    }

}