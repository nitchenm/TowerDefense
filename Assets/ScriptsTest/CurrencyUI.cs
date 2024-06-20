using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;

   // display currency 
    void Update()
    {
       currencyText.text = "$" + PlayerStats.Currency.ToString();
    }
}
