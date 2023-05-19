using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interactions : MonoBehaviour
{
    private float countDown = 120;

    public Text Timer;

    public GameObject GameOverPanel;

    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(countDown / 60);
            float seconds = Mathf.FloorToInt(countDown % 60);

            Timer.text = (minutes).ToString() + ":" + (seconds).ToString();
        }
        else
        {
            countDown = 0;
            float minutes = Mathf.FloorToInt(countDown / 60);
            float seconds = Mathf.FloorToInt(countDown % 60);
            Timer.text = (minutes).ToString() + ":" + (seconds).ToString();
            GameOverPanel.SetActive(true);
        }
    }

}
