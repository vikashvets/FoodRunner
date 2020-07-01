using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipsForFood : MonoBehaviour
{
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI NameText;
    private GameObject ImagePicture;
    public GameObject ParentPanel;
    public Button CloseButton;
    public Tip[] Tips;// Fill in the inspector
    // Start is called before the first frame update
    void Start()
    {//     The random appearance of any tip from the array after the game.
        int rand = Random.Range(0, Tips.Length);
        ImagePicture = Instantiate(Tips[rand].picture, new Vector3(0, 80, 0), Quaternion.identity);
        ImagePicture.transform.SetParent(ParentPanel.transform, false);
        DescriptionText.text = Tips[rand].description;
        NameText.text = Tips[rand].name;
        CloseButton.onClick.AddListener(CloseButtonClick);

    }

    // Update is called once per frame
    void Update()
    {
    }
    void CloseButtonClick()
    {

        ParentPanel.SetActive(false);
    }

    //Tips class
    [System.Serializable]
    public class Tip
    {
        public string description;
        public string name;
        public GameObject picture;
    }
}
