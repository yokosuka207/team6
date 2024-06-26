using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_SE : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 左
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }

        // 右
        if (Input.GetKey(KeyCode.RightArrow))
        {
            audioSource.PlayOneShot(sound2);
        }

        // 上
        if (Input.GetKey(KeyCode.UpArrow))
        {
            audioSource.PlayOneShot(sound3);
        }

        // 下
        if (Input.GetKey(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(sound4);
        }
    }
}
