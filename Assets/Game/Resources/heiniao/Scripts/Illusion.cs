using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illusion : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float ScaleChange = 0.05f;
    float ScalePerFrame = 1f;
    public Color color;

   // public SpawnIllusion Parent;

    [Header("尾巴的左移速度，是影响尾长的主要因素")]
    public float TailsSpeed = 0.05f;
    [Header("主体尾长的截取位置，可以根据数值不要最细的尾巴,0为尾巴留到最细，1为不留尾巴，不能大于初始尾宽")]
    public float TailsLong = 0f;
    [Header("每个影子的宽度缩减")]
    public float ScaleChangePerFrame = 0.05f;
    [Header("废弃：初始尾宽，可以大于1")]
    public float TailsWidth = 0.6f;
    [Header("每个影子的透明度减少量，0为不减，1为1帧直接不透明")]
    public float AlphaDownPerFrame = 0.02f;

    [Header("每个影子的下坠速度，可根据数值调节角度,正值向下，负值向上")]
    public float TailsDrop = 0.02f;



    void Start()
    {
        
        /*
        TailsSpeed = Parent.TailsSpeed;
        TailsLong = Parent.TailsLong;
        ScaleChangePerFrame = Parent.ScaleChangePerFrame; 
        TailsWidth = Parent.TailsWidth;
        AlphaDownPerFrame = Parent.AlphaDownPerFrame;*/



        color = GetComponent<SpriteRenderer>().color;
        ScaleChange = ScaleChangePerFrame;
        gameObject.transform.localScale = new Vector3(TailsWidth, TailsWidth, TailsWidth);
        //Destroy(gameObject, 1.5f);
    }

    void Update()
    {

        color.a-= AlphaDownPerFrame;
        

        GetComponent<SpriteRenderer>().color = color;



        ScalePerFrame -= ScaleChange;
        gameObject.transform.localScale -= new Vector3(ScaleChange, ScaleChange, ScaleChange);
        transform.Translate(-TailsSpeed, -TailsDrop, 0);

         if (ScalePerFrame <= TailsLong) Destroy(gameObject,0);


    }


}
