using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesUI : MonoBehaviour
{   
    public TextMeshProUGUI livesText;
    
    void Update()
    {
        livesText.text = PlayerStats.Lives + " Lives";
    }
}
