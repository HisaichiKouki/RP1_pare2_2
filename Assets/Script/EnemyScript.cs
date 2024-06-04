using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SearchableEditorWindow;

public class EnemyScript : MonoBehaviour
{
    [SerializeField, Header("éÄä[ÇÃÉvÉåÉnÉu")] private GameObject corpsePrefab;
    [SerializeField, Header("ìGÇÃHP")] private int hp;
    [SerializeField, Header("ìGÇÃà⁄ìÆë¨ìx")] private float speed;
    [SerializeField, Header("ìGÇÃçUåÇóÕ")] private int attackPower;
    [SerializeField, Header("ìGÇÃçUåÇä‘äu")] private float attackCoolTime;

    GameObject parent;

    bool isMove;
    bool serchMove;
    bool isAttack;

    int currentHP;
    int currentAttackPower;
    float attckCoolTimeCount;


    GameObject targetObj;
    Vector2 newVelocity;
    Rigidbody2D rigidbody;

    public GameObject hitPointBar;
    HitPointBarScript hitPointBarScript;


    public void SetSerchMove(bool set) { serchMove = set; }
    public bool GetSerchMove() { return serchMove; }
    public bool GetIsAttack() { return isAttack; }
    public void SetisAttack(bool set) { isAttack = set; }
    public void SetIsMove(bool set) { isMove = set; }

    public void SetTargetObj(GameObject setTargetObj) { targetObj = setTargetObj; }

    public void Damage(int value) { currentHP -= value; }
    // Start is called before the first frame update
    void Start()
    {
        speed *= -1;//ç∂Ç…ìÆÇ≠ÇΩÇﬂ
        parent = transform.parent.gameObject;
        currentHP = hp;
        isMove = true;
        serchMove = false;
        isAttack = false;
        hitPointBarScript = hitPointBar.GetComponent<HitPointBarScript>();
        hitPointBarScript.hitPoint = currentHP;

        currentAttackPower = attackPower;
        attckCoolTimeCount = attackCoolTime;

        rigidbody = parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         //Debug.Log("isMove=" + isMove + ",serch=" + serchMove + ",attack=" + isAttack);
        //Debug.Log("ìGHP=" + currentHP);

        Move();
        SerchMove();
        Attack();
        hitPointBarScript.hitPoint = currentHP;
    }


    void Move()
    {
        if (isAttack || !isMove)
        {
            return;
        }
        rigidbody.velocity = new Vector2(speed, 0);
    }

    void SerchMove()
    {
        if (!serchMove || targetObj == null) { return; }

        newVelocity = targetObj.transform.position - transform.position;

        rigidbody.velocity = newVelocity.normalized * -speed;
        TargetOfYado();
    }
    void Attack()
    {

        if (!isAttack) { return; }
        serchMove = false;
        rigidbody.velocity = Vector2.zero;

        //é©ï™ÇÃHPÇ™ñ≥Ç≠Ç»Ç¡ÇΩéû
        if (currentHP <= 0)
        {
            GameObject copse=Instantiate(corpsePrefab);
            copse.transform.position = transform.position;
            Destroy(parent.gameObject);
            return;
        }
        //ëäéËÇì|ÇµÇΩéû
        if (targetObj == null)
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
            return;
        }

        //çUåÇÇÃÉNÅ[ÉãÉ^ÉCÉÄÇ™écÇ¡ÇƒÇÈéû
        if (attckCoolTimeCount > 0)
        {
            attckCoolTimeCount -= Time.deltaTime;
            return;
        }

        //É_ÉÅÅ[ÉWÇó^Ç¶ÇÈ
        if (targetObj.transform.tag == "koyado")
        {
            targetObj.GetComponent<KoyadoScript>().Damage(currentAttackPower);
            //Debug.Log("ÉRÉÑÉhÇ…DamageÇó^Ç¶ÇΩÅI");
        }
        else if (targetObj.transform.tag == "Yado1")
        {
            YadoAttack();
        }
        else if (targetObj.transform.tag == "Yado2")
        {
            YadoAttack();
        }
        else if (targetObj.transform.tag == "Yado3")
        {
            YadoAttack();
        }
        else if (targetObj.transform.tag == "Hokora")
        {
            HokoraAttack();
        }else if(targetObj.transform.tag == "YadoHome")
        {
            targetObj.GetComponent<YadoHomeScript>().Damage(currentAttackPower);
            //Debug.Log("ÉÑÉhÉJÉäÇÃâ∆Ç…DamageÇó^Ç¶ÇΩÅI");
        }
        attckCoolTimeCount = attackCoolTime;

    }

    void YadoAttack()
    {
        if (targetObj.GetComponent<YadoScript>().GetIsHold())
        {
            targetObj.GetComponent<YadoScript>().NiceCover();
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
            targetObj = null;
            serchMove = false;
            return;
        }

        targetObj.GetComponent<YadoScript>().Damage(currentAttackPower);

        if (targetObj.GetComponent<YadoScript>().GetIsBroken())
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
            targetObj = null;
            serchMove = false;
        }
        //Debug.Log("ÉÑÉhÇ…DamageÇó^Ç¶ÇΩÅI");
    }
    void TargetOfYado()
    {
        if (targetObj.gameObject.tag== "Yado1"|| targetObj.gameObject.tag == "Yado2"|| targetObj.gameObject.tag == "Yado3")
        {
            if (targetObj.GetComponent<YadoScript>().GetIsHold())
            {
                attckCoolTimeCount = attackCoolTime;
                isAttack = false;
                isMove = true;
                targetObj = null;
                serchMove = false;
                return;
            }

            if (targetObj.GetComponent<YadoScript>().GetIsBroken())
            {
                attckCoolTimeCount = attackCoolTime;
                isAttack = false;
                isMove = true;
                targetObj = null;
                serchMove = false;
            }
        }
    }
    void HokoraAttack()
    {
        if (targetObj.GetComponent<HokoraScript>().GetIsHold())
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
            return;
        }
        targetObj.GetComponent<HokoraScript>().Damage(currentAttackPower);

        if (targetObj.GetComponent<HokoraScript>().GetIsBroken())
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
        }
        //Debug.Log("ÉzÉRÉâÇ…DamageÇó^Ç¶ÇΩÅI");
    }
}
