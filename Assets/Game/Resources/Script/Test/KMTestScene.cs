using UnityEngine;
using System.Collections;

public class KMTestScene : MonoBehaviour
{
    public KMLightMask lightMask;

    public int playerHpNum = 4;

    // Use this for initialization
    void Start()
    {
        lightMask.OnPlayerLifeChange(playerHpNum);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(playerHpNum > 0)
                playerHpNum--;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lightMask.OnPlayerLifeChange(playerHpNum);
        }
#endif
    }
}
