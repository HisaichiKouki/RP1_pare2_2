using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointShoutScript : MonoBehaviour
{

    [Header("観客の声をここで管理する")]
    public GameObject canvas;//キャンバス
    [SerializeField, Header("どんなタイプのテキストか")] private GameObject shoutObj;

    [SerializeField, Header("タイプごとのテキストの内容")] private string[] shoutTexts1;
    [SerializeField] private string[] shoutTexts2;
    [SerializeField] private string[] shoutTexts3;

    private bool[] isShout=new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
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

            }
        }
    }
}
