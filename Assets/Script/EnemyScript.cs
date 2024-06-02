using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

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
        parent = transform.parent.gameObject;
        currentHP = hp;
        isMove = true;
        serchMove = false;
        isAttack = false;

        currentAttackPower = attackPower;

        rigidbody =parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("isMove=" + isMove + ",serch=" + serchMove + ",attack=" + isAttack);
        Debug.Log("�GHP=" + currentHP);

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

        rigidbody.velocity = newVelocity.normalized * speed;

    }
    void Attack()
    {

        if (!isAttack) { return; }
        rigidbody.velocity = Vector2.zero;

        //������HP�������Ȃ�����
        if (currentHP <= 0)
        {
            Destroy(parent.gameObject);
            return;
        }
        //�����|������
        if (targetObj == null)
        {
            attckCoolTimeCount = 0;
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
            Debug.Log("�R���h��Damage��^�����I");
        }
        attckCoolTimeCount = attackCoolTime;
       
    }
}
