using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2CharaBehav : MonoBehaviour
{

    enum Chap2AnimaState
    {
        WaitState,
        AnimaState,
        NormalState
    };
    Chap2AnimaState chap2AnimaState=Chap2AnimaState.WaitState;

    // Start is called before the first frame update
    void Start()
    {
        SDistance = SphereSpeed * SpinTime;

        s1.transform.Translate(0.5f * (SDistance + S2_Radius), 0.866f * (SDistance + S2_Radius), 0);
        s2.transform.Translate(0.5f * (SDistance + S2_Radius), -0.866f * (SDistance + S2_Radius), 0);
        s3.transform.Translate(-(SDistance +S2_Radius), 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (chap2AnimaState == Chap2AnimaState.WaitState)
        {
            TimeCount++;
            if (TimeCount == WaitTime)
            {
                chap2AnimaState = Chap2AnimaState.AnimaState;
                TimeCount = -1;
            }


        }

        if (chap2AnimaState == Chap2AnimaState.AnimaState)
        {
            TimeCount++;

            if (TimeCount == SpinTime)
            {
                chap2AnimaState = Chap2AnimaState.NormalState;
            }
            else//否则转
            {

                s1.transform.Translate(-0.5f * SphereSpeed, -0.866f * SphereSpeed, 0);
                s2.transform.Translate(-0.5f * SphereSpeed, 0.866f * SphereSpeed, 0);
                s3.transform.Translate(SphereSpeed, 0, 0);


                s1.transform.RotateAround(s1.transform.parent.position, new Vector3(0f, 0f, 1f), -SpinSpeed);
                s2.transform.RotateAround(s2.transform.parent.position, new Vector3(0f, 0f, 1f), -SpinSpeed);
                s3.transform.RotateAround(s3.transform.parent.position, new Vector3(0f, 0f, 1f), -SpinSpeed);
            }

        }

        if (chap2AnimaState == Chap2AnimaState.NormalState)
        {
            s1.transform.RotateAround(s1.transform.parent.position, new Vector3(0f, 0f, 1f), -S1_Speed);
            s2.transform.RotateAround(s2.transform.parent.position, new Vector3(0f, 0f, 1f), -S2_Speed);
            s3.transform.RotateAround(s3.transform.parent.position, new Vector3(0f, 0f, 1f), -S3_Speed);

        }
    }

    public SphereBehaviour s1;
    public SphereBehaviour s2;
    public SphereBehaviour s3;


    public int WaitTime;

    int TimeCount=-1;

    public float SphereSpeed = 0;//小球往里缩的速度
    public int SpinTime = 0;//旋转时间（帧数）
    public float SpinSpeed = 0;//动画整体旋转速度
    public float S1_Radius = 0;//s1半径
    public float S2_Radius = 0;//s2半径
    public float S3_Radius = 0;//s3半径

    public float S1_Speed = 0;
    public float S2_Speed = 0;
    public float S3_Speed = 0;


    float SDistance;
    
}
