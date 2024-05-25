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
        float nowTime = Time.time - startTime - addTime;
        timerTex.text = "Time:" + (limitTime - nowTime).ToString("F2");
        scoreTex.text = "Score:" + score.ToString();
    }


    // �X�R�A�̉��Z
    public void AddScore(int value)
    {
        score += value;
    }

    // �X�R�A�̎擾
    public static int GetScore()
    {
        return score;
    }

    public void AddTime(float value)
    {
        addTime += value;
    }
}
