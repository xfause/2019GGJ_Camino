using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bgmove : MonoBehaviour
{
    public CharacterBehavior characterBehavior;
    float CharacterHorizontalSpeed = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CharacterHorizontalSpeed = characterBehavior.RunSpeed;
        if (!characterBehavior.ControlFlag) CharacterHorizontalSpeed = 0;//禁止操作时停止水平移动

        transform.Translate(CharacterHorizontalSpeed, 0, 0);

    }
}
