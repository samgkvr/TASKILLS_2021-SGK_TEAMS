using System.Collections.Generic;
using UnityEngine;

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
    private GameObject RightPanel;

    private void Awake()
    {
        RigthPanelToggleOnOff(false);
    }

    public void RigthPanelToggleOnOff(bool isOnOff)
    {
        RightPanel.SetActive(isOnOff);
    }
}
