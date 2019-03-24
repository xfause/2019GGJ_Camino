using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class Level0 : MonoBehaviour
{
    //private PlayMakerFSM fsm;

    public Image title;

    public SpriteRenderer player;

    public SpriteRenderer guangban;

    // Use this for initialization
    void Start()
    {
        //fsm = GetComponent<PlayMakerFSM>();
        title.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        guangban.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {

    }

    public void Fsm_ShowTitle()
    {
        title.gameObject.SetActive(true);
        title.DOFade(1f, 3f);
    }

    public void Fsm_ShowPlayer()
    {
        player.gameObject.SetActive(true);
        player.DOFade(1f, 3f);
    }

    public void Fsm_HideTitle()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(title.DOFade(0f, 3f));
        seq.AppendCallback(()=> { title.gameObject.SetActive(false); });
    }

    public void Fsm_ShowGuangBan()
    {
        guangban.gameObject.SetActive(true);
        guangban.DOFade(1f, 3f);
    }

    public void Fsm_MovePlayer()
    {
        player.transform.DOMoveX(15f, 3f).SetEase(Ease.InOutQuad);
    }

    public void Fsm_QuanPingShanGuang()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(player.DOFade(0f, 3f));
        seq.AppendCallback(() => 
        {
            player.gameObject.SetActive(false);
            Camera.main.DOColor(Color.white, 1f);
        });
    }

    public void Fsm_ChangeScene()
    {
        KMGlobal.Instance.curLevelID++;
        SceneManager.LoadScene(KMGlobal.Instance.curLevelID);
    }
}
