using UnityEngine;
using System.Collections;
using DG.Tweening;

public class KMLightMask : MonoBehaviour
{
    public float lightRangeChangeDuration = 1f;

    public float[] lightScaleRatio;

    // Use this for initialization
    void Start()
    {

    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPlayerLifeChange(int playerHpNum)
    {
        float fScaleRatio = 0f;
        switch (playerHpNum)
        {
            case 1:
                fScaleRatio = lightScaleRatio[0];
                break;
            case 2:
                fScaleRatio = lightScaleRatio[1];
                break;
            case 3:
                fScaleRatio = lightScaleRatio[2];
                break;
            case 4:
                fScaleRatio = lightScaleRatio[3];
                break;
            default:
                fScaleRatio = 1f;
                break;
        }
        transform.DOScale(fScaleRatio * Vector3.one, lightRangeChangeDuration);
    }
}
