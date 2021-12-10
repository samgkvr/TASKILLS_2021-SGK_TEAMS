using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lot : MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;

    [SerializeField]
    private GameObject RightPanel;

    private void Awake()
    {
        RigthPanelToggleOnOff(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RigthPanelToggleOnOff(bool isOnOff)
    {
        RightPanel.SetActive(isOnOff);
    }
}
