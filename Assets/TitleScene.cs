using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] public Text text;
    private float alpha;
    private bool plus;

    // Start is called before the first frame update
    void Start()
    {
        plus = false;
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //���炩�̃Q�[���̏���������Ƃ��܂� 

        // ���l��ύX
        if(plus)
        {
            alpha += 0.001f;
        }
        else
        {
            alpha -= 0.001f;
        }
        text.color = new Color(1.0f, 1.0f, 1.0f, alpha);

        if(alpha >= 1.0f || alpha <= 0)
        {
            plus = !plus;
        }

        if (Input.anyKeyDown) //���[�U�[���Ȃ�炩�̃L�[���}�E�X�{�^�����������ŏ��̃t���[���̂� true ��Ԃ��܂��i�ǂݎ���p�j
        {
            //Debug.Log("A key or mouse click has been detected");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
