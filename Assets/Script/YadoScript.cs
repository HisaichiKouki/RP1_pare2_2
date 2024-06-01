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
    private BoxCollider2D collider;
    PlayerScript playerScript;

    public void SetIsHold(bool set) { isHold = set; }
    void Start()
    {
        childObj = transform.GetChild(0).gameObject;
        childObj2 = transform.GetChild(1).gameObject;
        levelCount = 0;
        collider=GetComponent<BoxCollider2D>();
        playerScript=FindAnyObjectByType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
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
        }
    }

    void LevelUP()
    {
        if (levelCount != levelUpTime.Length)
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
