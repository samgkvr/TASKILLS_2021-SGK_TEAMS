using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject LocalPlayer;
    public User CurrentUser;
    private void Awake()
    {
        CurrentUser = LocalPlayer.GetComponent<User>();
    }
}
