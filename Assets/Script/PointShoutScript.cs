using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointShoutScript : MonoBehaviour
{

    [Header("観客の声をここで管理する")]
    public GameObject canvas;//キャンバス
    [SerializeField, Header("クールタイム")] private float setCoolTime;
    [SerializeField, Header("どんなタイプのテキストか")] private GameObject shoutObj;

    [SerializeField, Header("タイプごとのテキストの内容")] private string[] shoutTexts1;
    [SerializeField] private string[] shoutTexts2;
    [SerializeField] private string[] shoutTexts3;


    [SerializeField] private string[] shoutKoyadoLevelUP;


    private bool[] isShout=new bool[3];
    private bool koyadoLevelUp;

    float coolTimeCount;
    float koyadoCoolTimeCount;

    
    public void setShout(int value) { isShout[value] = true; }
    public void setKoyadoLevelUpShout() { koyadoLevelUp = true; }
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        coolTimeCount = 0;
        koyadoLevelUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        CoolTime();
        KoyadoCoolTime();
        if (koyadoCoolTimeCount<=0)
        {
            if (koyadoLevelUp)
            {
                if (shoutKoyadoLevelUP.Length > 0)
                {
                    //ここで乱数を生成
                    int rnd = UnityEngine.Random.Range(0, shoutKoyadoLevelUP.Length);
                    //プレハブのタイプによって出方が変わる
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //表示する言葉を乱数で決める
                    text.GetComponent<Text>().text = shoutKoyadoLevelUP[rnd];
                    //キャンバスを決めないと描画されない
                    text.transform.SetParent(canvas.transform, false);
                    //初期サイズを０に
                    text.transform.localScale = Vector3.zero;
                    koyadoLevelUp = false;
                    koyadoCoolTimeCount = setCoolTime*5;
                }
            }

        }
        if (coolTimeCount<=0)
        {
            if (isShout[0])
            {
                if (shoutTexts1.Length > 0)
                {
                    //ここで乱数を生成
                    int rnd = UnityEngine.Random.Range(0, shoutTexts1.Length);
                    //プレハブのタイプによって出方が変わる
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //表示する言葉を乱数で決める
                    text.GetComponent<Text>().text = shoutTexts1[rnd];
                    //キャンバスを決めないと描画されない
                    text.transform.SetParent(canvas.transform, false);
                    //初期サイズを０に
                    text.transform.localScale = Vector3.zero;
                    isShout[0] = false;
                    coolTimeCount = setCoolTime;
                }
            }
            else if (isShout[1])
            {
                if (shoutTexts2.Length > 0)
                {
                    //ここで乱数を生成
                    int rnd = UnityEngine.Random.Range(0, shoutTexts2.Length);
                    //プレハブのタイプによって出方が変わる
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //表示する言葉を乱数で決める
                    text.GetComponent<Text>().text = shoutTexts2[rnd];
                    //キャンバスを決めないと描画されない
                    text.transform.SetParent(canvas.transform, false);
                    //初期サイズを０に
                    text.transform.localScale = Vector3.zero;
                    isShout[1] = false;
                    coolTimeCount = setCoolTime;
                }
            }
            else if (isShout[2])
            {
                if (shoutTexts3.Length > 0)
                {
                    //ここで乱数を生成
                    int rnd = UnityEngine.Random.Range(0, shoutTexts3.Length);
                    //プレハブのタイプによって出方が変わる
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //表示する言葉を乱数で決める
                    text.GetComponent<Text>().text = shoutTexts3[rnd];
                    //キャンバスを決めないと描画されない
                    text.transform.SetParent(canvas.transform, false);
                    //初期サイズを０に
                    text.transform.localScale = Vector3.zero;
                    isShout[2] = false;
                    coolTimeCount = setCoolTime;
                }
            }

        }
        
    }

    void CoolTime()
    {
        if (coolTimeCount <= 0) return;
        coolTimeCount -= Time.deltaTime;
    }

    void KoyadoCoolTime()
    {
        if (koyadoCoolTimeCount <= 0) return;
        koyadoCoolTimeCount -= Time.deltaTime;
    }
}
