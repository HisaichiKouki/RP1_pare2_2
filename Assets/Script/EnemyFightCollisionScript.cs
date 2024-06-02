using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightCollisionScript : MonoBehaviour
{

    EnemyScript enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript=FindAnyObjectByType<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "koyado")
        {
            if (!enemyScript.GetIsAttack())
            {
                //攻撃モードにする
                enemyScript.SetTargetObj(collision.gameObject);
                enemyScript.SetSerchMove(false);
                enemyScript.SetIsMove(false);
                enemyScript.SetisAttack(true);

            }
        }else if(collision.gameObject.tag == "Yado1")
        {
            if (!enemyScript.GetIsAttack())
            {
                //攻撃モードにする
                enemyScript.SetTargetObj(collision.gameObject);
                enemyScript.SetSerchMove(false);
                enemyScript.SetIsMove(false);
                enemyScript.SetisAttack(true);

            }
        }
        else if (collision.gameObject.tag == "Yado2")
        {
            if (!enemyScript.GetIsAttack())
            {
                //攻撃モードにする
                enemyScript.SetTargetObj(collision.gameObject);
                enemyScript.SetSerchMove(false);
                enemyScript.SetIsMove(false);
                enemyScript.SetisAttack(true);

            }
        }
        else if (collision.gameObject.tag == "Yado3")
        {
            if (!enemyScript.GetIsAttack())
            {
                //攻撃モードにする
                enemyScript.SetTargetObj(collision.gameObject);
                enemyScript.SetSerchMove(false);
                enemyScript.SetIsMove(false);
                enemyScript.SetisAttack(true);

            }
        }
        else if (collision.gameObject.tag == "Hokora")
        {
            if (!enemyScript.GetIsAttack())
            {
                //攻撃モードにする
                enemyScript.SetTargetObj(collision.gameObject);
                enemyScript.SetSerchMove(false);
                enemyScript.SetIsMove(false);
                enemyScript.SetisAttack(true);

            }
        }
    }

   
}
