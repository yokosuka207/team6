using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text timerTex;
    [SerializeField] private Text scoreTex;
    private float startTime;
    private static int score;
    [SerializeField] private float limitTime;
    private float addTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        score = 0;
        addTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float nowTime = limitTime - (Time.time - startTime) + addTime;

        if (nowTime <= 0.0f)
        {
            nowTime = 0.0f;
            SceneManager.LoadScene("ResultScene");
        }

        timerTex.text = "Time:" + (nowTime).ToString("F2");
        scoreTex.text = "Score:" + score.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTime(-10);
            AddScore(1);
        }
    }


    // スコアの加算
    static public void AddScore(int value)
    {
        score += value;
    }

    // スコアの取得
    public static int GetScore()
    {
        return score;
    }

    public void AddTime(float value)
    {
        addTime += value;
    }
}
