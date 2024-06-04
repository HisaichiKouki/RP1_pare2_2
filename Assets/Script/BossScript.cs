using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleprefab;
    [SerializeField] private ParticleSystem particleprefab1;

    [SerializeField, Header("{XĚHP")] private int hitPoint;
    [SerializeField, Header("{XĚUÍ")] private int attackPower;
    [SerializeField, Header("{XĚUÔu")] private int attackCoolTime;

    [SerializeField, Header("{XĚUvnu")] private GameObject attackPrefab;
    [SerializeField, Header("{XĚUťčÔ")] private float attackTime;

    float[] isCoolTime = new float[8];
    //private GameObject hitObj;
    private GameObject[] targetObj = new GameObject[8];

    private GameObject setObj;
    public GameObject serachObj;
    BossAttackSerch serachScript;
    float resetTime;
    int isHitPoint;
    int nowTargetCount;

    public GameObject hitPointBar;
    HitPointBarScript hitPointBarScript;

    public void AddTargetCount() { if (nowTargetCount < 7) nowTargetCount++; }
    public void MinasTargetCount() { if (nowTargetCount > 0) nowTargetCount--; }
    public int GetTargetCount() { return nowTargetCount; }
    //public void SetTarget(GameObject set) { hitObj = set; }
    public GameObject GetTarget(int value) { return targetObj[value]; }

    int a;
    // Start is called before the first frame update
    void Start()
    {
        nowTargetCount = 0;
        serachScript = serachObj.GetComponent<BossAttackSerch>();
        isHitPoint = hitPoint;

        hitPointBarScript = hitPointBar.GetComponent<HitPointBarScript>();
        hitPointBarScript.hitPoint = isHitPoint;
    }


    // Update is called once per frame
    void Update()
    {
        if (serachScript.yadoObjects.Count > 0)
        {
            for (int i = 0; i < 8; i++)
            {

                if (targetObj[i] == null)
                {
                    Debug.Log("bossTargetCount=" + serachScript.yadoObjects.Count);

                    targetObj[i] = serachScript.yadoObjects[0];
                    serachScript.yadoObjects.RemoveAt(0);
                    break;
                }

            }
        }

        for (int i = 0; i < 8; i++)
        {
            Attack(i);

        }
        //SerachObjReset();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            a++;
        }
        hitPointBarScript.hitPoint = isHitPoint;

    }

    void Attack(int value)
    {
        if (isCoolTime[value] >= 0)
        {
            isCoolTime[value] -= Time.deltaTime;
            return;
        }

        if (targetObj[value] == null) { return; }

        GameObject attackObj = Instantiate(attackPrefab);
        attackObj.transform.position = targetObj[value].transform.position;
        attackObj.GetComponent<AttackScript>().SetAttackPower(attackPower);
        attackObj.GetComponent<AttackScript>().SetAttackTime(attackTime);
        attackObj.GetComponent<Renderer>().material.color = Color.red;
        //targetObj = null;
        isCoolTime[value] = attackCoolTime;
        return;

    }
    public void Damage(int value)
    {
        isHitPoint -= value;
        //Debug.Log("isHitPoint=" + isHitPoint);

        if (isHitPoint <= 0)
        {
            Destroy(transform.parent.gameObject);
            Debug.Log("{Xđ|ľ˝");
            particleprefab.Play();
            particleprefab1.Play();

        }
    }
    void SerachObjReset()
    {
        resetTime += Time.deltaTime;
        if (resetTime > 5.5f)
        {
            serachObj.SetActive(true);
            resetTime = 0;
        }
        else if (resetTime > 5.0f)
        {
            serachObj.SetActive(false);

        }
    }
}
