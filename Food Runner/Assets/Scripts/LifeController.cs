
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Life;
    public List<GameObject> hearts;
    private int lives = 4;
    private float x = 55f;
    private float y = -37f;
    private float z = 0f;
    private PlayerController GamePlayer;
    private MonsterController GameMonster;
    private LevelManager GameLevelManager;
    private RepeatBG[] BG;
    private GameObject[] Spawners;
    private GameObject[] Platforms;
    private MovingController[] MovingControllers;
    private Animator playerAnimation;
    private Animator monsterAnimation;
    public bool IsSetted = false;
    private AudioSource EffectsPlayerSourceDeath;

    private void Start()
    {
        GameMonster = FindObjectOfType<MonsterController>();
        GamePlayer = FindObjectOfType<PlayerController>();
        EffectsPlayerSourceDeath = GameObject.FindWithTag("death").GetComponent<AudioSource>();
        GameLevelManager = FindObjectOfType<LevelManager>();
        playerAnimation = GamePlayer.GetComponent<Animator>();
        monsterAnimation = GameMonster.GetComponent<Animator>();

        Vector3 Position = new Vector3(x, y, z);

        hearts = new List<GameObject>();

        for (int i = 0; i < lives; i++)
        {
            UpdateUIAdd();
        }
    }

    void Update()
    {
        if (hearts.Count == 0 && IsSetted != true)
        {
            IsSetted = true; //shows that the game is over
            //Sets death animation, play death sound
            monsterAnimation.SetBool("IsCatch", true);
            monsterAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
            EffectsPlayerSourceDeath.Play();
            //Stops everething: spawn, moving
            Spawners = GameObject.FindGameObjectsWithTag("spawner");
            MovingControllers = FindObjectsOfType<MovingController>();
            BG = FindObjectsOfType<RepeatBG>();
            for (int i = 0; i < Spawners.Length; i++)
            {
                Destroy(Spawners[i]);
            }
            for (int i = 0; i < MovingControllers.Length; i++)
            {
                MovingControllers[i].StopMoving();
            }
            for (int i = 0; i < BG.Length; i++)
            {
                BG[i].StopBG();
            }

            // Turns off platform colliders. If a player is standing on the platform at the time of loss, then he will fall to the ground.
            Platforms = GameObject.FindGameObjectsWithTag("platform");
            for (int i = 0; i < Platforms.Length; i++)
            {
                foreach (Collider2D c in Platforms[i].GetComponentsInChildren<Collider2D>())
                {
                    c.enabled = false;
                }

            }
            //Sets score and load EndGame after 3s
            PlayerPrefs.SetInt("score", GameLevelManager.score);
            Invoke("AnotherScene", 3);

        }

    }

    //Life add and lose Functions 
    public void UpdateUIAdd()
    {
        var heart = Instantiate(Life, new Vector3(x, y, z), Quaternion.identity) as GameObject;
        heart.transform.SetParent(canvas.transform, false);
        hearts.Add(heart);
        x += 50f;
    }
    public void UpdateUISub()
    {
        if (hearts.Count != 0)
        {
            Destroy(hearts.Last());
            hearts.Remove(hearts.Last());
            x -= 50f;
        }

    }

    public void AnotherScene()
    {
        SceneManager.LoadScene("EndGame");
    }
}