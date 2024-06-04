using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSerch : MonoBehaviour
{

    BossScript bossScript;
   // float restartTime;
    [SerializeField]private BoxCollider2D boxCollider;
    
    public List<GameObject> yadoObjects = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        bossScript = FindAnyObjectByType<BossScript>();
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //restartTime+=Time.deltaTime;
        //if (restartTime > 5.4f)
        //{
        //    //boxCollider.gameObject.SetActive(true);
        //    Debug.Log("ƒ{ƒX‚Ì“–‚½‚è”»’èÄ‹N“®");
        //    restartTime = 0;
        //}
        //else if (restartTime > 5)
        //{
        //    //boxCollider.gameObject.SetActive(false);

        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "koyado")
        {
            {
                // bossScript.SetTarget(collision.gameObject);
                yadoObjects.Add(collision.gameObject);
               // bossScript.AddTargetCount();

            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "koyado")
        //{
        //    if (bossScript.GetTarget(bossScript.GetTargetCount()) == null)
        //    {
        //        bossScript.SetTarget(collision.gameObject, bossScript.GetTargetCount());
        //        if (bossScript.GetTargetCount() < 7)
        //        {
        //            bossScript.AddTargetCount();
        //            Debug.Log("bossTargetCount=" + bossScript.GetTargetCount());
        //        }
        //    }
        //}
    }
}
