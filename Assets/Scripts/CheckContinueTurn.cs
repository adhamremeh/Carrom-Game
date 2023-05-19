using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckContinueTurn : MonoBehaviour
{

    public Text yourScore;
    public Text enemyScore;

    

    public bool CheckContinue(int preScore)
    {
        int score;


        if (!Strike_Bar.yourTurn)
        {
            score = int.Parse(yourScore.text);
        }
        else
        {
            score = int.Parse(enemyScore.text);
        }

        Debug.Log("pre " + preScore +" score" + score);
        Debug.Log("turn: " + Strike_Bar.yourTurn);
        
        if (score > preScore)
            return true;
        else
            return false;
    }
}
