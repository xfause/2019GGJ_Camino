using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{

    enum CharacterState
    {
        RunState,
        JumpState,        
        DropState,
        SlideState
    };

    [HideInInspector]
    public bool BumpState = false;//初始无碰撞，如果碰撞了可以通过修改一次该变量让其减速
    CharacterState characterState=CharacterState.RunState;//有限状态机初始为前进状态

    public BackgroundBehavior backgroundBehavior;//绑定一个背景，这里需要手动操作
    float BackgroundHeight = 0;//场地高度，这里只要这个高度，具体怎么产生这个高度由场地类产出,外部不可控
    float DeltaHeight = 0;


    // Start is called before the first frame update
    void Start()
    {
        RunSpeed = RunSpeedMax;
        JumpSpeed = JumpSpeedStart;
        BackgroundHeight = backgroundBehavior.BackgroundHeight;
        CharacterHeight = backgroundBehavior.BackgroundHeight;
        DeltaHeight = backgroundBehavior.DealtaHeight;

    }

    public SphereBehaviour s1;
    public SphereBehaviour s2;
    public SphereBehaviour s3;


    public bool ControlFlag = true;

    // Update is called once per frame
    void Update()
    {
        BackgroundHeight = backgroundBehavior.BackgroundHeight;
        //--------------------------按键输入------------------------
        if (Input.GetKeyDown(KeyCode.W)&& ControlFlag)  UpInput = true;
        if (Input.GetKeyDown(KeyCode.S) && ControlFlag)  DownInput = true; 
        if (Input.GetKeyDown(KeyCode.Space) && ControlFlag) SpaceInput = true;

        if (Input.GetKeyUp(KeyCode.W)) UpInput = false;
        if (Input.GetKeyUp(KeyCode.S)) DownInput = false;
        if (Input.GetKeyUp(KeyCode.Space)) SpaceInput = false;

        if (!ControlFlag) { UpInput = false; DownInput = false; SpaceInput = false; }


        //---------------------------输入变量-------------------------
        if (UpInput) {//输入向上时，输入修改状态
            
            if (characterState == CharacterState.RunState) {
                characterState = CharacterState.JumpState;
            }
        }


        if (DownInput){//输入向下时，输入修改状态
            if (characterState == CharacterState.RunState){
                characterState = CharacterState.DropState;
            }
        }
        else
        {
            if (characterState == CharacterState.SlideState)//贴地滑动时
            {
                 characterState = CharacterState.JumpState;
            }

        }

        if (SpaceInput) {//按下space时，输入修改状态
            //暂时没内容
        }
        //------------------------碰撞时--------------------------------

        if (BumpState)//碰撞时执行一次，减速至最低
        {
            RunSpeed = RunSpeedMin;
            s1.Bumped();
            BumpState = false;
        }
        //------------------------前进-----------------------------------
        transform.Translate(RunSpeed, 0, 0);//每帧正常的前进

        if (RunSpeed != RunSpeedMax) {//碰撞后会有减速,减速会逐渐回升
            RunSpeed += SpeedUpAfterBump;
            if (RunSpeed > RunSpeedMax) RunSpeed = RunSpeedMax;
        }
        if(!ControlFlag) transform.Translate(RunSpeed, 0.5f* RunSpeed, 0);//禁止控制时自己动，向上动


        //----------------------------上升------------------------------------
        if (characterState == CharacterState.JumpState)
        {
            if (CharacterHeight >= BackgroundHeight) {//在跳跃的上升过程中
                if (JumpTime == JumpMaxTime || JumpSpeed <= 0||!UpInput)//达到最高跳跃状态，进入下落状态
                {
                    characterState = CharacterState.DropState;
                    JumpSpeed = JumpSpeedStart;

                }
                
                else{//否则上升
                    transform.Translate(0, JumpSpeed, 0);
                    CharacterHeight += JumpSpeed;
                    JumpSpeed -= JumpSpeedDown;//减速，这里不判断，到上面一起判断


                }
            }
            else //在下坠后的回升
            {
                CharacterHeight += DropSpeed;
                transform.Translate(0, DropSpeed, 0);
                DropSpeed += DropSpeedUp;
                if (CharacterHeight >= BackgroundHeight)//如果上升多了则回弹
                {
                    transform.Translate(0, BackgroundHeight - CharacterHeight, 0);
                    CharacterHeight = BackgroundHeight;
                    characterState = CharacterState.RunState;
                    DropSpeed = 0;
                }
            }


        }
        //------------------------------下坠---------------------------------
        if (characterState == CharacterState.DropState)
        {
            if (CharacterHeight > BackgroundHeight)//跳跃后从高处下落
            {
                CharacterHeight -= DropSpeed;
                transform.Translate(0, -DropSpeed, 0);
                DropSpeed += DropSpeedUp;
                if (CharacterHeight <= BackgroundHeight)//如果下落多了则回弹
                {
                    transform.Translate(0, BackgroundHeight - CharacterHeight, 0);
                    
                    CharacterHeight = BackgroundHeight;
                    characterState = CharacterState.RunState;
                    DropSpeed = 0;
                }

            }
            else //因为操作下坠
            {
                CharacterHeight -= DropSpeed;
                transform.Translate(0, -DropSpeed, 0);
                DropSpeed += DropSpeedUp;
                if (CharacterHeight <= BackgroundHeight - DropDistance)//如果到头则贴地
                {
                    transform.Translate(0, BackgroundHeight - CharacterHeight - DropDistance, 0);
                    CharacterHeight = BackgroundHeight - DropDistance;
                    characterState = CharacterState.SlideState;
                    DropSpeed = 0;
                }


            }

        }

        //----------------------------前进状态时---------------------------------------
        if (characterState == CharacterState.RunState)
        {
            transform.Translate(0, DeltaHeight, 0);
            CharacterHeight += DeltaHeight;


        }
        //---------------------------贴地滑行时-----------------------------------
        if (characterState == CharacterState.SlideState)
        {
            transform.Translate(0, DeltaHeight, 0);
            CharacterHeight += DeltaHeight;

        }

    }

    //输入
    bool UpInput=false;
    bool DownInput=false;
    bool SpaceInput=false;

    //前进与碰撞相关数值
    [Header("最高前进速度，也就是平常的速度")]
    public float RunSpeedMax = 0;
    [Header("碰撞减速后的最低值")]
    public float RunSpeedMin = 0;
    [Header("碰撞后的回升速度时的加速度")]
    public float SpeedUpAfterBump = 0;
    [HideInInspector]
    public float RunSpeed = 0;//由于有个减速回升的过程，所以会有个实时速度，外部不可控


    //跳跃相关数值
    float CharacterHeight = 0;//实时得到高度，用于与场地高度进行对比，用来判断是在上升操作中还是下落操作中，外部不可控
    [Header("跳跃的下落加速度,具体指的是向下的加速度")]
    public float JumpSpeedDown = 0;
    [Header("跳跃初速度")]
    public float JumpSpeedStart = 0;
    float JumpSpeed = 0;//跳跃速度,外部不可控
    [Header("跳跃最长时间(帧数)，和两个速度共同决定了跳跃的最高高度")]
    public int JumpMaxTime = 0;
    int JumpTime = 0;//用于实时计时，外部不可控

    //下坠相关数值
    [Header("下坠加速度")]
    public float DropSpeedUp=0;
    [Header("下坠最大距离，需要提前试好")]
    public float DropDistance = 0;
   
    float DropSpeed=0;//下坠速度,外部不可控


}
