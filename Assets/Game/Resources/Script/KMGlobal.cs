using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class KMGlobal : MonoBehaviour
{
    /// <summary>
    /// 关卡ID
    /// </summary>
    public int curLevelID = 0;

    private static KMGlobal _sInstance;
    public static KMGlobal Instance
    {
        get
        {
            if (_sInstance == null)
            {
                _sInstance = FindObjectOfType(typeof(KMGlobal)) as KMGlobal;
                if(_sInstance)
                {
                    _sInstance.Init();
                    DontDestroyOnLoad(_sInstance);
                }
            }

            if (_sInstance == null)
            {
                var obj = new GameObject("KMGlobal");
                _sInstance = obj.AddComponent<KMGlobal>();
                _sInstance.Init();
                DontDestroyOnLoad(_sInstance);
                Debug.Log("KMGlobal object not exist. will Generate Automatically.");
            }

            return _sInstance;
        }
    }

    public void Init()
    {
        Reset();
    }

    public void Reset()
    {

    }

    private void Update()
    {
        //Debug.Log("当前关卡ID" + curLevelID);
    }
}
