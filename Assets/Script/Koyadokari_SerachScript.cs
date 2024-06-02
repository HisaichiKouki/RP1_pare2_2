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
        koyadoScript =FindAnyObjectByType<KoyadoScript>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Yado")
        {
            if (koyadoScript.GetSerchMove() == false)
            {
                koyadoScript.SetMoveTargetObj(collision.gameObject);
                koyadoScript.SetSerchMove(true);

            }
        }
    }
}
