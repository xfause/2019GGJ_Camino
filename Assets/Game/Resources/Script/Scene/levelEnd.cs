using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour
{
    public Text levelEndText;

    public float textAniDuration = 5f;

    public float textDisappearDelay = 1f;

    // Use this for initialization
    void Start()
    {
        //levelEndText.DOText("人生尔启程，吾之梦携家之梦，勤勉奋进！往昔赴异日。——青春芳华，我，与爸妈。", 5f);

        string endingText = "";
        switch (KMGlobal.Instance.curLevelID)
        {
            case 0:
                break;
            case 1:
                endingText = "人生尔启程，吾之梦携家之梦，勤勉奋进！往昔赴异日。——青春芳华，我，与爸妈。";
                break;
            case 2:
                endingText = "卒业止韶华，市井苦牵父母忧，唯情解伤，低首嗅铜臭。——重责而立，我、她，与爸妈。";
                break;
            case 3:
                endingText = "登峰已造极，利熏心源生养息，勿忘初心？回眸亲在否？——既业已成？孩子与她……";
                break;
            default:
                break;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(levelEndText.DOText(endingText, textAniDuration));
        seq.AppendInterval(textDisappearDelay);
        seq.AppendCallback(() =>
        {
            if (KMGlobal.Instance.curLevelID < 3)
            {
                ++KMGlobal.Instance.curLevelID;
                SceneManager.LoadScene(KMGlobal.Instance.curLevelID);
            }
            else
            {
                Debug.Log("游戏结束");
            }
        });
            
    }

    // Update is called once per frame
    void Update()
    {

    }
}
