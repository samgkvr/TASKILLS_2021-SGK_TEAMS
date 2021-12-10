using UnityEngine;

public class Lot : MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;
    public int Cost;

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
