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
        //���炩�̃Q�[���̏���������Ƃ��܂� 
     
        if (Input.anyKeyDown) //���[�U�[���Ȃ�炩�̃L�[���}�E�X�{�^�����������ŏ��̃t���[���̂� true ��Ԃ��܂��i�ǂݎ���p�j
        {
            //Debug.Log("A key or mouse click has been detected");
            SceneManager.LoadScene("GameScene");
        }
    }
}
