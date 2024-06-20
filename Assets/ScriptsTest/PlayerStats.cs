using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Currency;
    public int startCurrency = 400;

    public static int Lives;
    public int startLives = 20;

    void Start(){
        Currency = startCurrency;
        Lives = startLives;
    }

}
