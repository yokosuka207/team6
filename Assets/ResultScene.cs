using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ゲームシーンで保持しているスコアを取得
        int score = GameManager.GetScore();

        scoreText.text = score.ToString();

        //何らかのゲームの処理があるとします 
        if (Input.anyKeyDown)
        {
            //Debug.Log("A key or mouse click has been detected");
            SceneManager.LoadScene("TitleScene");
        }
    }
}
