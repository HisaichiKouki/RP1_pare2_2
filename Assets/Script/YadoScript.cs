using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YadoScript : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject childObj;
    GameObject childObj2;
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

    public bool GetIsBroken() { return isBroken; }
    public void SetIsHold(bool set) { isHold = set; }
    void Start()
    {
        childObj = transform.GetChild(0).gameObject;
        childObj2 = transform.GetChild(2).gameObject;//シャッターつきの画像
        levelCount = 0;
        collider = GetComponent<CapsuleCollider2D>();
        playerScript = FindAnyObjectByType<PlayerScript>();
        isHitPoint = hitpoint;
        isBroken = false;
        respownCount = 0;
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
                    childObj2.SetActive(true);
                    collider.isTrigger = false;
                }
                else
                {
                    childObj2.SetActive(false);
                    collider.isTrigger = true;
                }
                childObj.SetActive(true);
                collider.enabled = true;

                if (Input.GetKeyDown(KeyCode.G))
                {
                    Damage();
                }
            }
        }
        else
        {

            Respown();
        }
        

    }

    void Damage()
    {
        if (levelCount >= 0)
        {
            isHitPoint--;
            Debug.Log("isHitPoint=" + isHitPoint);

            if (isHitPoint <= 0)
            {
                levelCount--;
                isHitPoint = hitpoint;
                isBroken = true;

                Debug.Log("isBroken");
                collider.enabled = false;
                childObj.SetActive(false);
                childObj2.SetActive(false);
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
        if (levelCount != levelUpTime.Length&& levelCount>=0)
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
}
