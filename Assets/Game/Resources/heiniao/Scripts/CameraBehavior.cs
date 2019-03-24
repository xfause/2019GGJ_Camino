using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         DeltaHeight = backgroundBehavior.DealtaHeight;

    }

    // Update is called once per frame
    void Update()
    {
        BackgroundHeight = backgroundBehavior.BackgroundHeight;
        CharacterHorizontalSpeed = characterBehavior.RunSpeed;


        if (!characterBehavior.ControlFlag) CharacterHorizontalSpeed = 0;//禁止操作时停止水平移动
        transform.Translate(CharacterHorizontalSpeed, DeltaHeight, 0);

    }

    public BackgroundBehavior backgroundBehavior;//绑定一个背景，这里需要手动操作
    float BackgroundHeight = 0;//场地高度，这里只要这个高度，具体怎么产生这个高度由场地类产出,外部不可控


    public CharacterBehavior characterBehavior;
    float CharacterHorizontalSpeed = 0;
    float DeltaHeight = 0;
}
