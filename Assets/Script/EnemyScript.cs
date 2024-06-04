using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SearchableEditorWindow;

public class EnemyScript : MonoBehaviour
{
    [SerializeField, Header("���[�̃v���n�u")] private GameObject corpsePrefab;
    [SerializeField, Header("�G��HP")] private int hp;
    [SerializeField, Header("�G�̈ړ����x")] private float speed;
    [SerializeField, Header("�G�̍U����")] private int attackPower;
    [SerializeField, Header("�G�̍U���Ԋu")] private float attackCoolTime;

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
        speed *= -1;//���ɓ�������
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
        //Debug.Log("�GHP=" + currentHP);

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

        //������HP�������Ȃ�����
        if (currentHP <= 0)
        {
            GameObject copse=Instantiate(corpsePrefab);
            copse.transform.position = transform.position;
            Destroy(parent.gameObject);
            return;
        }
        //�����|������
        if (targetObj == null)
        {
            attckCoolTimeCount = attackCoolTime;
            isAttack = false;
            isMove = true;
            return;
        }

        //�U���̃N�[���^�C�����c���Ă鎞
        if (attckCoolTimeCount > 0)
        {
            attckCoolTimeCount -= Time.deltaTime;
            return;
        }

        //�_���[�W��^����
        if (targetObj.transform.tag == "koyado")
        {
            targetObj.GetComponent<KoyadoScript>().Damage(currentAttackPower);
            //Debug.Log("�R���h��Damage��^�����I");
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
            //Debug.Log("���h�J���̉Ƃ�Damage��^�����I");
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
        //Debug.Log("���h��Damage��^�����I");
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
        //Debug.Log("�z�R����Damage��^�����I");
    }
}
