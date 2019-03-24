using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    public KMLightMask lightMask;

    // Start is called before the first frame update
    void Start()
    {
        //Num = 3;
        color = GetComponent<SpriteRenderer>().color;
        if(lightMask)
        {
            lightMask.OnPlayerLifeChange(Num+1);
        }

        if (Num < SphereTag)
        {
            color.a = 0f;

            GetComponent<SpriteRenderer>().color = color;

        }


    }

    // Update is called once per frame
    void Update()
    {

        if (transform.rotation.z > 360) transform.Rotate(0, 0, -360f);
        if (Num < SphereTag)
        {
            color.a -= 0.04f;

            GetComponent<SpriteRenderer>().color = color;
            if (color.a <= 0f) color.a = 0;
        }

    }

    public void Bumped()
    {
        if (Num > -1)
            Num--;

        //if(Num == -1)
        //{
        //    Debug.Log("ssss");
        //}

        if(lightMask)
        {
            if(Num == -1)
            {
                //lightMask.OnPlayerLifeChange(0);
            }
            else
            {
                lightMask.OnPlayerLifeChange(Num+1);
            }
        }
    }

    SpriteRenderer spriteRenderer;

    public Color color;


    [Header("不可修改")]
    public int SphereTag=0;

    [HideInInspector]
    public static int Num = 3;
}
