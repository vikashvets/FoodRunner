using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InfoScreen : MonoBehaviour
{
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI NameText;
    private GameObject ImagePicture;
    public GameObject ParentPanel;
    public Button CloseButton;
    public InfoTip[] Tips;
    public InfoTip PressedObject;
    // Start is called before the first frame update
    void Start()
    {   // For all info tips button
        CloseButton.onClick.AddListener(CloseButtonClick);
        foreach (InfoTip Tip in Tips)
        {
            Tip.but.onClick.AddListener(TipButtonClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //the same, control with back button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(ParentPanel.active)
            {
                ParentPanel.SetActive(false);
                Destroy(ImagePicture);
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }
    }
    public void TipButtonClick()
    {
        //  Finding the object that you clicked on, displaying it on the tips panel
        string nameButton = EventSystem.current.currentSelectedGameObject.name;
        foreach (InfoTip Tip in Tips)
        {
            if(Tip.name == nameButton)
            {
                PressedObject = Tip;
            }
        }
        ImagePicture = Instantiate(PressedObject.picture, new Vector3(0, 80, 0), Quaternion.identity);
        ImagePicture.transform.SetParent(ParentPanel.transform, false);
        DescriptionText.text = PressedObject.description;
        NameText.text = PressedObject.name;
        ParentPanel.SetActive(true);
    }
    void CloseButtonClick()
    {
        ParentPanel.SetActive(false);
        Destroy(ImagePicture);
    }
    //Tips class
    [System.Serializable]
    public class InfoTip
    {
        
        public string name;
        public string description;
        public GameObject picture;
        public Button but;
    }
}
