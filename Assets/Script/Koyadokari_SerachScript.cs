using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koyadokari_SerachScript : MonoBehaviour
{
    GameObject parentObj;
    KoyadoScript koyadoScript;


    // Start is called before the first frame update
    void Start()
    {

        parentObj = transform.parent.gameObject;
        koyadoScript = FindAnyObjectByType<KoyadoScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //センサーにヤドが当たったら
        if (collision.gameObject.tag == "Yado1")
        {

            if (!koyadoScript.GetYadoNum(0)&& koyadoScript.GetSerchMove() == false)
            {
                //サーチモードにする
                koyadoScript.SetMoveTargetObj(collision.gameObject);
                koyadoScript.SetSerchMove(true);
                koyadoScript.SetIsMove(false);
               
            }

        }
        else //センサーにヤドが当たったら
        if (collision.gameObject.tag == "Yado2")
        {

            if (!koyadoScript.GetYadoNum(1) && koyadoScript.GetSerchMove() == false)
            {
                //サーチモードにする
                koyadoScript.SetMoveTargetObj(collision.gameObject);
                koyadoScript.SetSerchMove(true);
                koyadoScript.SetIsMove(false);
               // koyadoScript.SetYadoNum(1, true);
            }

        }

    }

    


}
