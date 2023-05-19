using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    public Text yourScore;
    public Text enemyScore;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "BlackPuck")
        {
            coll.gameObject.GetComponent<PucksControl>().destroyPuck();
            int enemyScoreINT = int.Parse(enemyScore.text) + 1;
            enemyScore.text = enemyScoreINT.ToString();
        }
        else if (coll.gameObject.tag == "WhitePuck")
        {
            coll.gameObject.GetComponent<PucksControl>().destroyPuck();
            int yourScoreINT = int.Parse(yourScore.text) + 1;
            yourScore.text = yourScoreINT.ToString();
        }
        else if (coll.gameObject.tag == "Queen")
        {
            coll.gameObject.GetComponent<PucksControl>().destroyPuck();
            if (Strike_Bar.yourTurn)
            {
                int yourScoreINT = int.Parse(yourScore.text) + 2;
                yourScore.text = yourScoreINT.ToString();
            }
            else
            {
                int enemyScoreINT = int.Parse(enemyScore.text) + 2;
                enemyScore.text = enemyScoreINT.ToString();
            }
        }
        else if (coll.gameObject.tag == "yourStriker")
        {
            coll.gameObject.GetComponent<Animator>().Play("PucksDestroy");
            StrikerController.IntoPocketBool = true;
        }
        else if (coll.gameObject.tag == "enemyStriker")
        {
            coll.gameObject.GetComponent<Animator>().Play("PucksDestroy");
            EnemyAI.IntoPocketBool = true;
        }
    }
}
