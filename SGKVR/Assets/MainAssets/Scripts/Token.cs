using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    public int ID;
    public Item Item;
    public string Name;
    public string Description;
    public int Cost;
    public List<User> UserOwner;
    public User UserCreator;

    public bool OnSale;

    [SerializeField]
    private TMP_Text textName;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text textCost;
    [SerializeField]
    private TMP_Text textUserCreator;
    [SerializeField]
    private TMP_Text textUserOwner;
    [SerializeField]
    private TMP_Text textDescription;

    [SerializeField]
    private GameObject buttonBuy;

    [SerializeField]
    private GameObject LeftPanel;

    [SerializeField]
    private GameObject RightPanel;

    private GameManager m_GameManager;

    private User CurrentUser;

    private void Awake()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        m_GameManager = GameManager.GetComponent<GameManager>();
        CurrentUser = m_GameManager.CurrentUser;
        LeftPanel.SetActive(false);
        RightPanel.SetActive(false);
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        textName.text = Name;
        image.sprite = Item.Image;
        textCost.text = "Цена: " + Cost.ToString() + " ETH";
        textUserCreator.text = "Создатель: " + UserCreator.name;
        foreach (User go in UserOwner)
        {
            textUserOwner.text = go.Nickname;
        }
        textDescription.text = Description;

        if (OnSale)
        {
            foreach (User go in UserOwner)
            {
                if (go == CurrentUser)
                {
                    buttonBuy.SetActive(false);
                }
                else
                {
                    buttonBuy.SetActive(false);
                }
            }
        }
        else
        {
            foreach (User go in UserOwner)
            {
                if (go == CurrentUser)
                {
                    buttonBuy.SetActive(false);
                }
                else
                {
                    buttonBuy.SetActive(true);
                }
            }
        }
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

    public void bntBuy()
    {
        if (Cost <= CurrentUser.Money)
        {
            CurrentUser.OwnedToken.Add(this);
            UserOwner.Add(CurrentUser);
            CurrentUser.Money -= Cost;
            UpdateInfo();
        }
    }

    private void bntIsOnOff()
    {

    }
}
