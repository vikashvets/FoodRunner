
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    bool DoubleJumpAllowed = false;
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private Animator playerAnimation;
    private LifeController GameLifeController;
    private AudioSource EffectsPlayerSourceHero;

    //Function that determines if tap has hit the UI - object.
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        GameLifeController = FindObjectOfType<LifeController>();
        EffectsPlayerSourceHero = GameObject.FindWithTag("jump").GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {

        if (GameLifeController.IsSetted != true)//Determines if the game is not over
        {
            //Checking if the player touches the ground, the animation is set accordingly
            isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
            playerAnimation.SetBool("OnGround", isTouchingGround);
            if (!IsPointerOverUIObject())
            {//Jump and double jumps check
                isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
                playerAnimation.SetBool("OnGround", isTouchingGround);
                if (isTouchingGround)
                {
                    DoubleJumpAllowed = true;
                    playerAnimation.SetBool("OnGround", isTouchingGround);
                }
                playerAnimation.SetBool("OnGround", isTouchingGround);
                if (Input.touchCount > 0 & Input.GetTouch(0).phase == TouchPhase.Began && isTouchingGround)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                    EffectsPlayerSourceHero.Play(); //Jump sound play
                    playerAnimation.SetBool("OnGround", isTouchingGround);
                }
                if (DoubleJumpAllowed && Input.touchCount > 0 & Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                    EffectsPlayerSourceHero.Play();
                    DoubleJumpAllowed = false;
                    playerAnimation.SetBool("OnGround", isTouchingGround);

                }

                playerAnimation.SetBool("OnGround", isTouchingGround);
            }

        }
        else
        {

            playerAnimation.SetBool("OnGround", isTouchingGround);
        }

        if (GameLifeController.IsSetted == true)//Set animation after death
        {
            playerAnimation.SetBool("IsDead", true);
            playerAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        //Control for the desktop
        /*
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if(isTouchingGround)
        {
            DoubleJumpAllowed = true;
        }
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            EffectsPlayerSourceHero.Play();
        }
        else if (DoubleJumpAllowed && Input.GetButtonDown ("Jump"))
        {
             rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
             EffectsPlayerSourceHero.Play();
             DoubleJumpAllowed = false;
        }
        playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
       */

    }



}
