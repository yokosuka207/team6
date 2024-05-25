using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dirt")
        {       
            Debug.Log("dirt");
        }

        if (collision.gameObject.tag == "Gem")
        {
            Debug.Log("gem");
        }

        if (collision.gameObject.tag == "Stone")
        {
            Debug.Log("stone");
        }
        }
}
