using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Token : MonoBehaviour
{
    public int ID;
    public Item Item;
    public string Name;
    public string Description;
    public int Cost;
    public List<User> UserOwner;
    public User UserCreator;

    [SerializeField]
    private TMP_Text textName;
    [SerializeField]
    private TMP_Text textCost;
    [SerializeField]
    private TMP_Text textUserCreator;
    [SerializeField]
    private TMP_Text textUserOwner;
    [SerializeField]
    private TMP_Text textDescription;

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private GameObject LeftPanel;

    [SerializeField]
    private GameObject RightPanel;

    private void Awake()
    {
        LeftPanel.SetActive(false);
        RightPanel.SetActive(false);
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        textName.text = Name;
        textCost.text = "Цена: " + Cost.ToString() + " ETH";
        textUserCreator.text = "Создатель: " + UserCreator.name;
        //textUserOwner.text = UserOwner.;
        textDescription.text = Description;
    }

    public void LeftPanelToggle()
    {
        if (LeftPanel.activeSelf)
        {
            LeftPanel.SetActive(false);
        }
        else
        {
            LeftPanel.SetActive(true);
        }
    }

    public void RigthPanelToggle()
    {
        if (RightPanel.activeSelf)
        {
            RightPanel.SetActive(false);
        }
        else
        {
            RightPanel.SetActive(true);
        }
    }
}
