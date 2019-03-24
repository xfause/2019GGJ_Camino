using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
        chara = GameObject.Find("GameObject").GetComponent<CharacterBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bumped)
        {
            color.a -= 0.05f;

            GetComponent<SpriteRenderer>().color = color;

        }
    }

    bool IsBumped = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsBumped)
        {
            chara.BumpState = true;
            bumped = true;
        }
        //Destroy(gameObject);
       // Rigidbody2D temop = GetComponent<Rigidbody2D>();
       // temop.
    }

    SpriteRenderer spriteRenderer;

    public Color color;


    //private void OnCollisionEnter2D(Collision2D collision)de
    //{
    //    chara.BumpState = true;

    //}
    /*
    private void OnCollisionEnter(Collision collision)
    {
    Debug.Log("dd");
    }*/
    public CharacterBehavior chara;

    bool bumped = false;
}
