using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text timerTex;
    [SerializeField] private Text scoreTex;
    private float startTime;
    private static int score;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerTex.text = "Time:" + (Time.time - startTime).ToString("F2");
        scoreTex.text = "Score:" + score.ToString();
    }


    // スコアの加算
    public void AddScore(int value)
    {
        score += value;
    }

    // スコアの取得
    static public int GetScore()
    {
        return score;
    }
}
