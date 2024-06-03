using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class YadoSuctionScript : MonoBehaviour
{
    KoyadoScript koyadoScript;
    YadoScript yadoScript;
    private int thisYadoNum;
    // Start is called before the first frame update
    void Start()
    {
        //koyadoScript = FindAnyObjectByType<KoyadoScript>();
        yadoScript = transform.parent.GetComponent<YadoScript>();
        thisYadoNum = yadoScript.GetYadoNum();
        if (transform.parent.gameObject.tag == "Yado1")
        {
            thisYadoNum = 0;
        }
        else if (transform.parent.gameObject.tag == "Yado2")
        {
            thisYadoNum = 1;
        }
        else if (transform.parent.gameObject.tag == "Yado3")
        {
            thisYadoNum = 2;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "koyado")
        {
            koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();

            if (!koyadoScript.GetIsAttack())
            {
                //コヤドスクリプトのヤドで成長したかのフラグがfalseなら処理
                if (!koyadoScript.GetYadoNum(thisYadoNum))
                {
                    koyadoScript.SetIsMove(false);
                    koyadoScript.SetSerchMove(true);
                    koyadoScript.SetTargetObj(transform.gameObject);
                }
            }
        }
    }

}
