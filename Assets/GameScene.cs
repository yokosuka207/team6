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
        Invoke("ChangeScene", 1.5f); //���b��Ɏ��s����
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //���炩�̃Q�[���̏���������Ƃ��܂�


    }

    void ChangeScene()
    {
        SceneManager.LoadScene("ResultScene"); //�؂�ւ������V�[�������w�肷�邱�ƂŃV�[����؂�ւ��邱�Ƃ��ł��܂�
        
        //���܂�
        //���̃��\�b�h�͌Ăяo���ꂽ�u�ԂɃV�[���̓ǂݍ��݂��s���܂����ǂݍ��ރV�[�����d����ΐؑ֎��ɕ��ׂ������胆�[�U�[�ɂƂ��Ă͂�������Ƃ�������ɂȂ�\��������܂��B
        //���̂��߁ASceneManager.LoadSceneAsync���g���Ȃǔ񓯊��������s�����Ƃŉ��������\��������ł��傤�B
        //SceneManager.LoadSceneAsync���g���ƒʏ�̏������s���Ȃ��痠�ŃV�[����ǂݍ��݁A�ǂݍ��݂��I������^�C�~���O�ŃV�[����؂�ւ���Ƃ����`�ɂȂ�܂��B
        //�����̃��\�[�X���K�͂ȃQ�[���J����z�肵�Ă���ꍇ�͂����������_�ɒ��ӂ��K�v�ł��ˁB
    }

    public static int GetScore()
    {
        return Score;
    }
}
