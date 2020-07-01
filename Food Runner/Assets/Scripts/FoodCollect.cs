using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoodCollect : MonoBehaviour
{
    public LevelManager GameLevelManager;
    private LifeController GameLifeController;
    private MonsterController GameMonster;
    private AudioSource EffectsPlayerSourceUseful;
    private AudioSource EffectsPlayerSourceBonus;
    private AudioSource EffectsPlayerSourceJunk;


    void Start()
    {
        GameLevelManager = FindObjectOfType<LevelManager>();
        GameMonster = FindObjectOfType<MonsterController>();
        GameLifeController = FindObjectOfType<LifeController>();
        EffectsPlayerSourceUseful = GameObject.FindWithTag("useful").GetComponent<AudioSource>();
        EffectsPlayerSourceJunk = GameObject.FindWithTag("junk").GetComponent<AudioSource>();
        EffectsPlayerSourceBonus = GameObject.FindWithTag("bonus_sound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other is PolygonCollider2D)
        {
            Destroy(gameObject);
            if (gameObject.tag == "healthy_food")
            {

                GameLevelManager.UsefulItemsCollected++;
                GameLevelManager.ChangeScore(10);
                EffectsPlayerSourceUseful.Play(0);
                GameLevelManager.BonusTextCreateFunction(0, gameObject.transform);

            }
            else if (gameObject.tag == "bonus")
            {
                GameLevelManager.UsefulItemsCollected++;
                GameLevelManager.ChangeScore(50);
                EffectsPlayerSourceBonus.Play(0);
                for (int i = 0; i < 5; i++)
                {
                    GameLevelManager.BonusTextCreateFunction(i, gameObject.transform);
                }

            }
            else if (gameObject.tag == "junk_food")
            {
                if (GameLifeController.IsSetted != true) // check end game
                {
                    GameLifeController.UpdateUISub(); // minus life
                    GameMonster.MonsterMove(); // monster became closer
                    EffectsPlayerSourceJunk.Play(0); // play junk effect
                    GameLevelManager.MinusLifeCreateFunction(0, gameObject.transform);// animation with broken heart
                }
            }
            else if (gameObject.tag == "healthy_food_set")
            {
                GameLevelManager.UsefulItemsCollected++;
                GameLevelManager.ChangeScore(20);
                EffectsPlayerSourceUseful.Play(0);
                for (int i = 0; i < 2; i++)
                {
                    GameLevelManager.BonusTextCreateFunction(i, gameObject.transform);
                }
            }
            else if (gameObject.tag == "trap")
            {
                if (GameLifeController.IsSetted != true)
                {
                    GameLifeController.UpdateUISub();
                    GameMonster.MonsterMove();
                    EffectsPlayerSourceJunk.Play(0);
                    GameLevelManager.MinusLifeCreateFunction(0, gameObject.transform);
                }
            }

            if (GameLevelManager.UsefulItemsCollected >= 50) // add life after 50 useful items
            {
                if (GameLifeController.hearts.Count < 4) // add life only if lives count < 4
                {
                    GameLifeController.UpdateUIAdd();//add life
                    GameLevelManager.UsefulItemsCollected = 0;
                    GameMonster.MonsterMoveBack();//monster back
                    GameLevelManager.BonusLifeCreateFunction(0);
                }
                else
                {
                    GameLevelManager.UsefulItemsCollected = 0;
                }

            }


        }



    }


}
