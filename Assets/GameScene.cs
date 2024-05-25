using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    protected static int Score = 0;

    // Start is called before the first frame update
    // Use this for initialization
    void Start()
    {
        Invoke("ChangeScene", 1.5f); //数秒後に実行する
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //何らかのゲームの処理があるとします


    }

    void ChangeScene()
    {
        SceneManager.LoadScene("ResultScene"); //切り替えたいシーン名を指定することでシーンを切り替えることができます
        
        //おまけ
        //このメソッドは呼び出された瞬間にシーンの読み込みを行いますが読み込むシーンが重ければ切替時に負荷がかかりユーザーにとってはもっさりとした動作になる可能性があります。
        //そのため、SceneManager.LoadSceneAsyncを使うなど非同期処理を行うことで解消される可能性があるでしょう。
        //SceneManager.LoadSceneAsyncを使うと通常の処理を行いながら裏でシーンを読み込み、読み込みが終わったタイミングでシーンを切り替えるという形になります。
        //多くのリソースや大規模なゲーム開発を想定している場合はこういった点に注意が必要ですね。
    }

    public static int GetScore()
    {
        return Score;
    }
}
