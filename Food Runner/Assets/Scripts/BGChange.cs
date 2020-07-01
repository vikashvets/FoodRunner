using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGChange : MonoBehaviour
{
    public GameObject[] Backgrounds;
    public Color[] DarkPallete; // pallete for 1-st BG
    public Color[] BrightPallete; // pallete for 2-nd BG
    private GameObject NextBG;
    private Color RandColor;
    public bool IsTimeToChangeBackground = false;
    public bool IsChangePerformed = false;
    // Start is called before the first frame update
    void Start()
    {   //Start settings
        StartCoroutine(ChangeBoolBackground());
        Backgrounds[0].GetComponent<SpriteRenderer>().color = DarkPallete[Random.Range(0, DarkPallete.Length)];
        Backgrounds[0].GetComponent<SpriteRenderer>().color = new Color(Backgrounds[0].GetComponent<SpriteRenderer>().color.r, Backgrounds[0].GetComponent<SpriteRenderer>().color.g, Backgrounds[0].GetComponent<SpriteRenderer>().color.b, 1);
        NextBG = Backgrounds[1];
        RandColor = BrightPallete[Random.Range(0, BrightPallete.Length)];
        NextBG.GetComponent<SpriteRenderer>().color = RandColor;
        NextBG.GetComponent<SpriteRenderer>().color = new Color(NextBG.GetComponent<SpriteRenderer>().color.r, NextBG.GetComponent<SpriteRenderer>().color.g, NextBG.GetComponent<SpriteRenderer>().color.b, 0);
        NextBG.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTimeToChangeBackground)
        {

            if (!IsChangePerformed)
            {
                var t = Mathf.InverseLerp(0, 10000, 1);
                NextBG.GetComponent<SpriteRenderer>().color = Color.Lerp(NextBG.GetComponent<SpriteRenderer>().color, new Color(RandColor.r, RandColor.g, RandColor.b, 255), t); // smooth opacity change with time (from 0 to 1, to make it visible)

                if (NextBG.GetComponent<SpriteRenderer>().color.a >= 1f) // when opacity = 1 change performed, new background visible 
                {
                    IsChangePerformed = true;
                    t = 0;
                }

            }
            else
            {

                if (NextBG == Backgrounds[0]) // Now NextBG is current showing background 
                {
                    NextBG.GetComponent<SpriteRenderer>().sortingOrder = 0; // if current background is background[0] move it a layer below to place the next layer on top.
                    NextBG = Backgrounds[1]; // Setting the next background, now NextBG - it is realy next background
                    RandColor = BrightPallete[Random.Range(0, BrightPallete.Length)]; //Select a random color from the palette of this background.

                }
                else
                {
                    NextBG.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    NextBG = Backgrounds[0];
                    RandColor = DarkPallete[Random.Range(0, DarkPallete.Length)];
                }
                NextBG.GetComponent<SpriteRenderer>().color = new Color(RandColor.r, RandColor.g, RandColor.b, 0); // set transparent color
                NextBG.GetComponent<SpriteRenderer>().sortingOrder = 1;// put next bg on top layer
                IsTimeToChangeBackground = false;
                IsChangePerformed = false;//exit from this update loop
                StartCoroutine(ChangeBoolBackground());// timer again

            }

        }
    }
    public IEnumerator ChangeBoolBackground() // wait 60 seconds and tell, thst it is time to change background
    {
        yield return new WaitForSeconds(60);
        IsTimeToChangeBackground = true;

    }
}
