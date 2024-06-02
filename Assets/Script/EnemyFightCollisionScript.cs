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
        if (collision.gameObject.tag == "Enemy")
        {
            //if (koyadoScript.GetSerchMove() == false)
            //{
            //    //サーチモードにする
            //    koyadoScript.SetTargetObj(collision.gameObject);
            //    koyadoScript.SetSerchMove(true);
            //    koyadoScript.SetIsMove(false);

            //}
        }
    }
}
