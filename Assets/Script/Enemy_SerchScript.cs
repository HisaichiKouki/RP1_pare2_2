using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SerchScript : MonoBehaviour
{
    EnemyScript enemyScript;
    KoyadoScript koyadoScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = transform.parent.GetChild(0).GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //�U�����ĂȂ���
        if (!enemyScript.GetIsAttack())
        {
            //�Z���T�[�ɓG������������

            if (collision.gameObject.tag == "koyado")
            {
                koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();

                if (!koyadoScript.GetIsAttack())
                {

                    koyadoScript.SetIsMove(false);
                    koyadoScript.SetSerchMove(true);
                    koyadoScript.SetTargetObj(transform.parent.GetChild(0).gameObject);

                }
                if (enemyScript.GetSerchMove() == false)
                {
                    //�T�[�`���[�h�ɂ���
                    enemyScript.SetTargetObj(collision.gameObject);
                    enemyScript.SetSerchMove(true);
                    enemyScript.SetIsMove(false);

                }
            }
            else if (collision.gameObject.tag == "Hokora")
            {

                if (enemyScript.GetSerchMove() == false)
                {
                    //�T�[�`���[�h�ɂ���
                    enemyScript.SetTargetObj(collision.gameObject);
                    enemyScript.SetSerchMove(true);
                    enemyScript.SetIsMove(false);

                }
            }
            //�Z���T�[�Ƀ��h������������
            else if (collision.gameObject.tag == "Yado1")
            {

                if (enemyScript.GetSerchMove() == false)
                {
                    //�T�[�`���[�h�ɂ���
                    enemyScript.SetTargetObj(collision.gameObject);
                    enemyScript.SetSerchMove(true);
                    enemyScript.SetIsMove(false);

                }

            }
            else //�Z���T�[�Ƀ��h������������
            if (collision.gameObject.tag == "Yado2")
            {

                if (enemyScript.GetSerchMove() == false)
                {
                    //�T�[�`���[�h�ɂ���
                    enemyScript.SetTargetObj(collision.gameObject);
                    enemyScript.SetSerchMove(true);
                    enemyScript.SetIsMove(false);
                    // enemyScript.SetYadoNum(1, true);
                }

            }
            else //�Z���T�[�Ƀ��h������������
            if (collision.gameObject.tag == "Yado3")
            {

                if (enemyScript.GetSerchMove() == false)
                {
                    //�T�[�`���[�h�ɂ���
                    enemyScript.SetTargetObj(collision.gameObject);
                    enemyScript.SetSerchMove(true);
                    enemyScript.SetIsMove(false);
                    // enemyScript.SetYadoNum(1, true);
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
