using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //何らかのゲームの処理があるとします 
     
        if (Input.anyKeyDown) //ユーザーがなんらかのキーかマウスボタンを押した最初のフレームのみ true を返します（読み取り専用）
        {
            //Debug.Log("A key or mouse click has been detected");
            SceneManager.LoadScene("GameScene");
        }
    }
}
