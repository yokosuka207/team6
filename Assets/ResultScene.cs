using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���V�[���ŕێ����Ă���X�R�A���擾
        int score = GameScene.GetScore();

        //���炩�̃Q�[���̏���������Ƃ��܂� 
        if (Input.anyKeyDown)
        {
            //Debug.Log("A key or mouse click has been detected");
            SceneManager.LoadScene("TitleScene");
        }
    }
}
