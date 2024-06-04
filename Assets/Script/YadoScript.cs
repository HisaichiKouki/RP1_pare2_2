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
    private int levelCount;//���݂̃��x��
    private CapsuleCollider2D collider;
    PlayerScript playerScript;
    KoyadoScript koyadoScript;
    [SerializeField] private int hitpoint;
    private int isHitPoint;

    bool isBroken;
    [SerializeField] private float respownTime;
    float respownCount;
    [SerializeField, Header("���X�|�[������n�_")] private Vector2 respownPos;

    [SerializeField, Header("�f�o�b�O�p�A�������x��")] private int intlevel;

    [SerializeField, Header("���[��H�ׂ����ɑ�����o���l")] private float addXPpoint;

    [SerializeField, Header("���h�̃��x���\��")] private GameObject[] yadoLebelTex;
    int yadoNum;

    [SerializeField, Header("�����L�����o�X")] GameObject shoutCanpas;
    GameObject playerObj;
    Vector3 newPosition;

    public int GetYadoNum() { return yadoNum; }
    public bool GetIsBroken() { return isBroken; }
    public bool GetIsHold() { return isHold; }

    public void SetIsHold(bool set) { isHold = set; }

    
    public int GetLevel() { return levelCount; }
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        childObj = transform.GetChild(0).gameObject;
        levelCount = intlevel;
        for (int i = 0; i < yadoLebelTex.Length; i++)
        {
            yadoLebelTex[i].SetActive(false);
        }
        yadoLebelTex[levelCount].SetActive(true);
        collider = transform.GetComponent<CapsuleCollider2D>();
        playerScript = FindAnyObjectByType<PlayerScript>();
        // koyadoScript = FindAnyObjectByType<KoyadoScript>();
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
        else if (this.transform.tag == "Yado3")
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
                newPosition = playerObj.transform.position;
                newPosition.z = 0;
                transform.position = newPosition;
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
        SetShoutCanpas();
        shoutCanpas.transform.GetChild(0).GetComponent<PointShoutScript>().setShout(1);

        isHitPoint -= value;
        //Debug.Log("isHitPoint=" + isHitPoint);

        if (isHitPoint <= 0)
        {
            if (levelCount > 0)
            {
                levelCount--;
                isHitPoint = hitpoint;
                for (int i = 0; i < yadoLebelTex.Length; i++)
                {
                    yadoLebelTex[i].SetActive(false);
                }
                yadoLebelTex[levelCount].SetActive(true);
            }
            else
            {
                isBroken = true;

                Debug.Log("<color=cyan>YadoBroken=</color>");
                collider.enabled = false;
                childObj.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
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
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
            for (int i = 0; i < yadoLebelTex.Length; i++)
            {
                yadoLebelTex[i].SetActive(false);
            }
            yadoLebelTex[levelCount].SetActive(true);
        }
    }

    void LevelUP()
    {
        if (levelCount < levelUpTime.Length && levelCount >= 0)
        {
            xpCount += Time.deltaTime;
            //���h�̌o���l�����x���敪�̗ʂɒB�����烌�x���A�b�v����
            if (xpCount >= levelUpTime[levelCount])
            {
                SetShoutCanpas();
                shoutCanpas.transform.GetChild(0).GetComponent<PointShoutScript>().setShout(0);

                xpCount = 0;
                levelCount++;

                for (int i = 0; i < yadoLebelTex.Length; i++)
                {
                    yadoLebelTex[i].SetActive(false);
                }
                yadoLebelTex[levelCount].SetActive(true);
                //Debug.Log("nowLevel=" + levelCount);
                Debug.Log("<color=cyan>nowLevel=</color>" + levelCount + ",Yadonum=" + yadoNum);

            }
        }
    }
    public void AddXP()
    {
        xpCount += addXPpoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "koyado")
        {
            koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();
            // KoyadoScript koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();

            //�܂��������h�ɓ������ĂȂ������x���A�b�v����
            if (!koyadoScript.GetYadoNum(yadoNum))
            {
                koyadoScript.AddLevel(levelCount);
                if (koyadoScript.GetComponent<KoyadoScript>().GetNowLevelUP())
                {
                    SetShoutCanpas();
                    shoutCanpas.transform.GetChild(0).GetComponent<PointShoutScript>().setKoyadoLevelUpShout();
                    koyadoScript.GetComponent<KoyadoScript>().SetNowLevelUP(false);
                }


                koyadoScript.SetSerchMove(false);
                koyadoScript.SetIsMove(true);
                koyadoScript.SetTargetObj(null);

                koyadoScript.SetYadoNum(yadoNum, true);
                //collision.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
                Debug.Log("yadonum=" + yadoNum);
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
        //if (collision.gameObject.tag == "koyado")
        //{
        //    //KoyadoScript koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();
        //    koyadoScript = collision.gameObject.GetComponent<KoyadoScript>();
        //    if (!koyadoScript.GetYadoNum(yadoNum))
        //    {
        //        //�������Ń��h�Ń��x���A�b�v�����t���O��true�ɂ���(�s�ӂ̃t���O�����h������)
        //        koyadoScript.SetYadoNum(yadoNum, true);
        //        //koyadoScript.AddLevel(levelCount);
        //        koyadoScript.SetSerchMove(false);
        //        koyadoScript.SetIsMove(true);
        //        koyadoScript.SetTargetObj(null);
        //        //collision.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
        //    }
        //}

    }

    public void NiceCover()
    {
        SetShoutCanpas();
        shoutCanpas.transform.GetChild(0).GetComponent<PointShoutScript>().setShout(2);

    }
    void SetShoutCanpas()
    {
        Vector3 newPosition = shoutCanpas.transform.position;
        newPosition.x=transform.position.x;
        shoutCanpas.transform.position = newPosition;
    }
}
