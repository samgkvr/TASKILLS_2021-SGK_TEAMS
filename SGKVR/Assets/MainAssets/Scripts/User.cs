using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public int ID;
    public string Nickname;
    public string Email;
    public string Password;
    public string Description;
    public List<Token> OwnedToken;
    public List<Token> CreatedToken;
    public int Money;

    public bool ValidPassword(string m_password)
    {
        if (m_password == Password)
        {
            return true;
        }
        return false;
    }
}
