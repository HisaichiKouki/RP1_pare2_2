using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField, Header("死骸のプレハブ")] private GameObject corpsePrefab;
    [SerializeField, Header("敵のHP")] private int hp;
    [SerializeField, Header("敵の移動速度")] private float speed;
    [SerializeField, Header("敵の攻撃力")] private int attackPower;
    [SerializeField, Header("敵の攻撃間隔")] private float attackCoolTime;

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
        speed *= -1;//左に動くため
        parent = transform.parent.gameObject;
        currentHP = hp;
        isMove = true;
        serchMove = false;
        isAttack = false;

        currentAttackPower = attackPower;
        attckCoolTimeCount = attackCoolTime;

        rigidbody = parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         Debug.Log("isMove=" + isMove + ",serch=" + serchMove + ",attack=" + isAttack);
        //Debug.Log("敵HP=" + currentHP);

        Move();
        SerchMove();
        Attack();

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

    }
    void Attack()
    {

        if (!isAttack) { return; }
        rigidbody.velocity = Vector2.zero;

        //自分のHPが無くなった時
        if (currentHP <= 0)
        {
            GameObject copse=Instantiate(corpsePrefab);
            copse.transform.position = transform.position;
            Destroy(parent.gameObject);
            return;
        }
        //相手を倒した時
        if (targetObj == null)
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
            return;
        }

        //攻撃のクールタイムが残ってる時
        if (attckCoolTimeCount > 0)
        {
            attckCoolTimeCount -= Time.deltaTime;
            return;
        }

        //ダメージを与える
        if (targetObj.transform.tag == "koyado")
        {
            targetObj.GetComponent<KoyadoScript>().Damage(currentAttackPower);
            Debug.Log("コヤドにDamageを与えた！");
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
            Debug.Log("ヤドカリの家にDamageを与えた！");
        }
        attckCoolTimeCount = attackCoolTime;

    }

    void YadoAttack()
    {
        if (targetObj.GetComponent<YadoScript>().GetIsHold())
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
            return;
        }
        targetObj.GetComponent<YadoScript>().Damage(currentAttackPower);

        if (targetObj.GetComponent<YadoScript>().GetIsBroken())
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
        }
        Debug.Log("ヤドにDamageを与えた！");
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
        Debug.Log("ホコラにDamageを与えた！");
    }
}
