using System.Collections;
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
    public float Cost;
    public User UserOwner;
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
    private GameObject buttonSell;

    //[SerializeField]
    //private TMP_Text textSellInput;

    [SerializeField]
    private GameObject LeftPanel;

    [SerializeField]
    private GameObject RightPanel;

    [SerializeField]
    private GameObject PurchaseFailedPanel;
    [SerializeField]
    private GameObject PurchaseCompletedPanel;
    [SerializeField]
    private GameObject SellMenu;

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
        textUserCreator.text = "Создатель: " + UserCreator.Nickname;
        textUserOwner.text = "Владелец: " + UserOwner.Nickname;
        textDescription.text = Description;

        if (!OnSale)
        {
            if (UserOwner == CurrentUser)
            {
                buttonBuy.SetActive(false);
            }
            else
            {
                buttonBuy.SetActive(false);
            }
        }
        else
        {
            if (UserOwner == CurrentUser)
            {
                buttonBuy.SetActive(false);
                buttonSell.SetActive(true);
            }
            else
            {
                buttonBuy.SetActive(true);
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

    public void SellMenuToggle()
    {
        if (SellMenu.activeSelf)
        {
            SellMenu.SetActive(false);
        }
        else
        {
            SellMenu.SetActive(true);
        }
    }

    public void bntBuy()
    {
        if (Cost <= CurrentUser.Money && CurrentUser != UserOwner)
        {
            CurrentUser.OwnedToken.Add(this);
            UserOwner = CurrentUser;
            CurrentUser.Money -= Cost;
            StartCoroutine(PurchaseCompleted());
            UpdateInfo();
        }
        else
        {
            StartCoroutine(PurchaseFailed());
        }
    }

    public void btnInfo()
    {
        LeftPanelToggle();
    }

    public void btnBid()
    {
        RigthPanelToggle();
    }

    public void btnSell()
    {
        SellMenuToggle();
    }

    public void btnSellPanel()
    {
        SellMenuToggle();
    }

    private IEnumerator PurchaseFailed()
    {
        PurchaseFailedPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        PurchaseFailedPanel.SetActive(false);
    }

    private IEnumerator PurchaseCompleted()
    {
        PurchaseCompletedPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        PurchaseCompletedPanel.SetActive(false);
    }

    private void bntIsOnOff()
    {

    }
}
