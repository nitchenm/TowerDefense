using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayNowButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mapa Grande");
    }
}
