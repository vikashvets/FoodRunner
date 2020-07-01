using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingDown : MonoBehaviour
{
    public bool IsButtonDownPressed = false;
    public Button DownButton;
    // Start is called before the first frame update
    void Start()
    {
        DownButton.onClick.AddListener(DownButtonClick);

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.tag == "platform")
        {

            if (IsButtonDownPressed)//If the player is standing on the platform and the drop button is pressed
            {
                // Disabling Colliders
                GameObject offedObject = other.gameObject.transform.root.gameObject;
                foreach (PolygonCollider2D c in offedObject.GetComponentsInChildren<PolygonCollider2D>())
                {
                    c.enabled = false;
                }
                IsButtonDownPressed = false;
                StartCoroutine(CollidersActivate(offedObject.GetComponentsInChildren<PolygonCollider2D>()));
            }

        }
        else
        {
            IsButtonDownPressed = false;
        }

    }
    IEnumerator CollidersActivate(PolygonCollider2D[] offedColliders)
    {
        yield return new WaitForSeconds(1);
        foreach (PolygonCollider2D c in offedColliders)
        {
           // c.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "platform")
        {
            IsButtonDownPressed = false;
        }


    }
    public void DownButtonClick()
    {
        IsButtonDownPressed = true;
    }
}
