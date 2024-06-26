using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private bool gameEnded = false;

    void Update()
    {
        if(gameEnded){
          return;
        }
        if(PlayerStats.Lives<= 0){
            EndGame();
        }
    }

    void EndGame(){
        gameEnded=true;
        Debug.Log("Game Over");
    }
}
