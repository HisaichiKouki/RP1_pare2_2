using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koyadokari_SerachScript : MonoBehaviour
{
    //public GameObject parentObj;
    KoyadoScript koyadoScript;


    // Start is called before the first frame update
    void Start()
    {

        //parentObj = transform.parent.gameObject;
        koyadoScript = FindAnyObjectByType<KoyadoScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�Z���T�[�ɓG������������
        if (collision.gameObject.tag == "Enemy")
        {
            if (koyadoScript.GetSerchMove() == false)
            {
                //�T�[�`���[�h�ɂ���
                koyadoScript.SetTargetObj(collision.gameObject);
                koyadoScript.SetSerchMove(true);
                koyadoScript.SetIsMove(false);

            }
        }
        //�Z���T�[�Ƀ��h������������
        else if (collision.gameObject.tag == "Yado1")
        {

            if (!koyadoScript.GetYadoNum(0)&& koyadoScript.GetSerchMove() == false)
            {
                //�T�[�`���[�h�ɂ���
                koyadoScript.SetTargetObj(collision.gameObject);
                koyadoScript.SetSerchMove(true);
                koyadoScript.SetIsMove(false);
               
            }

        }
        else //�Z���T�[�Ƀ��h������������
        if (collision.gameObject.tag == "Yado2")
        {

            if (!koyadoScript.GetYadoNum(1) && koyadoScript.GetSerchMove() == false)
            {
                //�T�[�`���[�h�ɂ���
                koyadoScript.SetTargetObj(collision.gameObject);
                koyadoScript.SetSerchMove(true);
                koyadoScript.SetIsMove(false);
               // koyadoScript.SetYadoNum(1, true);
            }

        }
        else //�Z���T�[�Ƀ��h������������
        if (collision.gameObject.tag == "Yado3")
        {

            if (!koyadoScript.GetYadoNum(2) && koyadoScript.GetSerchMove() == false)
            {
                //�T�[�`���[�h�ɂ���
                koyadoScript.SetTargetObj(collision.gameObject);
                koyadoScript.SetSerchMove(true);
                koyadoScript.SetIsMove(false);
                // koyadoScript.SetYadoNum(1, true);
            }

        }

    }

    


}
