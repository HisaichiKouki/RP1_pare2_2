using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YadoScript : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject childObj;
    private bool isHold;
    public float[] levelUpTime;
    private float xpCount;
    private int levelCount;//現在のレベル
    private CapsuleCollider2D collider;
    PlayerScript playerScript;

    [SerializeField] private int hitpoint;
    private int isHitPoint;

    bool isBroken;
    [SerializeField] private float respownTime;
    float respownCount;
    [SerializeField, Header("リスポーンする地点")] private Vector2 respownPos;

    [SerializeField, Header("デバッグ用、初期レベル")] private int intlevel;

    int yadoNum;

    public bool GetIsBroken() { return isBroken; }
    public bool GetIsHold() { return isHold; }

    public void SetIsHold(bool set) { isHold = set; }

    public int GetLevel() { return levelCount; }
    void Start()
    {
        childObj = transform.GetChild(0).gameObject;
        levelCount = intlevel;
        collider = GetComponent<CapsuleCollider2D>();
        playerScript = FindAnyObjectByType<PlayerScript>();
        isHitPoint = hitpoint;
        isBroken = false;
        respownCount = 0;

        if (this.transform.tag == "Yado1")
        {
            yadoNum = 0;
        }
        else if (this.transform.tag == "Yado2")
        {
            yadoNum = 1;
        }
        else if (this.transform.tag == "Yado2")
        {
            yadoNum = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isBroken)
        {
            if (isHold)
            {
                LevelUP();

                childObj.SetActive(false);
                collider.enabled = false;
            }
            else
            {


                if (playerScript.GetIsHold())
                {
                    collider.isTrigger = false;
                }
                else
                {
                    collider.isTrigger = true;
                }
                childObj.SetActive(true);
                collider.enabled = true;


            }
        }
        else
        {

            Respown();
        }


    }

    public void Damage(int value)
    {
        if (levelCount >= 0)
        {
            isHitPoint -= value;
            Debug.Log("isHitPoint=" + isHitPoint);

            if (isHitPoint <= 0)
            {
                levelCount--;
                isHitPoint = hitpoint;
                isBroken = true;

                Debug.Log("isBroken");
                collider.enabled = false;
                childObj.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }

    }

    void Respown()
    {
        if (!isBroken) { return; }
        respownCount += Time.deltaTime;
        if (respownCount >= respownTime)
        {
            transform.position = respownPos;
            isBroken = false;
            respownCount = 0;
            isHitPoint = hitpoint;
            collider.enabled = true;
            childObj.SetActive(true);
            levelCount = 0;
        }
    }

    void LevelUP()
    {
        if (levelCount != levelUpTime.Length && levelCount >= 0)
        {
            xpCount += Time.deltaTime;
            //ヤドの経験値がレベル区分の量に達したらレベルアップする
            if (xpCount >= levelUpTime[levelCount])
            {
                xpCount = 0;
                levelCount++;
                Debug.Log("nowLevel=" + levelCount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "koyado")
        {
            KoyadoScript koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();

            //まだ同じヤドに当たってない時レベルアップする
            if (!koyadoScript.GetYadoNum(yadoNum))
            {
                koyadoScript.AddLevel(levelCount);

                koyadoScript.SetSerchMove(false);
                koyadoScript.SetIsMove(true);
                koyadoScript.SetTargetObj(null);
                collision.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
            }


        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "koyado")
        //{
        //    KoyadoScript koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();

        //    koyadoScript.SetSerchMove(false);
        //    koyadoScript.SetIsMove(true);
        //    koyadoScript.SetMoveTargetObj(null);
        //    collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "koyado")
        {

            KoyadoScript koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();

            if (!koyadoScript.GetYadoNum(yadoNum))
            {
                //こっちでヤドでレベルアップしたフラグをtrueにする(不意のフラグ操作を防ぐため)
                koyadoScript.SetYadoNum(yadoNum, true);
                //koyadoScript.AddLevel(levelCount);
                koyadoScript.SetSerchMove(false);
                koyadoScript.SetIsMove(true);
                koyadoScript.SetTargetObj(null);
                collision.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
