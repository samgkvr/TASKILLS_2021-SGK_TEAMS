using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandUX : MonoBehaviour
{
    private GameManager m_GameManager;
    private User CurrentUser;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject oneBtn;

    [SerializeField]
    private TMP_Text textNickname;
    [SerializeField]
    private TMP_Text textMoney;
    [SerializeField]
    private TMP_Text textOwnedToken;

    private void Awake()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        m_GameManager = GameManager.GetComponent<GameManager>();
        CurrentUser = m_GameManager.CurrentUser;
        panel.SetActive(false);
        oneBtn.SetActive(true);
    }

    float totalTime = 0f;

    private void Update()
    {
        if (totalTime <= 0)
        {
            UpdateUX();
            totalTime = 2f;
        }
        totalTime -= Time.deltaTime;
    }

    private void UpdateUX()
    {
        if (CurrentUser.Nickname != null || CurrentUser.Nickname != "" || CurrentUser != null)
        {
            textNickname.text = CurrentUser.Nickname;
            textMoney.text = "Баланс ETC: " + CurrentUser.Money.ToString();
            textOwnedToken.text = "Куплено картин: " + CurrentUser.OwnedToken.Count.ToString();
        }
        else
        {
            textNickname.text = "Вы не авторизованы";
            textMoney.text = "";
            textOwnedToken.text = "";
        }
    }

    public void btnPanelToggle()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            oneBtn.SetActive(true);
        }
        else
        {
            panel.SetActive(true);
            oneBtn.SetActive(false);
        }
    }
}
