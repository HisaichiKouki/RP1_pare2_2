using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class KoyadoScript : MonoBehaviour
{
    GameObject parent;

    [SerializeField, Header("�R���h�̑���")] private float speed;
    [SerializeField, Header("�R���h��Hp")] private int hp;
    [SerializeField, Header("�R���h�̍U����")] private int[] attackPower;
    [SerializeField, Header("�R���h�̍U���Ԋu")] private float attackCoolTime;
    [SerializeField, Header("�f�o�b�O�p�A�R���h�������x��")] private int initlevel;
    //[SerializeField] GameObject target;
    bool alive = true;

    bool isMove;
    bool serchMove;
    bool isAttack;

    int currentHP;
    int currentAttackPower;
    float attckCoolTimeCount;

    bool[] YadoNum = new bool[3];


    int currentLevel;

    private Rigidbody2D rigidbody;

    GameObject targetObj;
    Vector2 newVelocity;


    //�q���h�̃Z���T�[�����h�A�������͓G����������true�ɂ��A�^�[�Q�b�g�̕����ɂ�����
    //���h���q���h�ɓ�����A�������͓G�Ɛ퓬�ɂȂ�����false��
    public void SetSerchMove(bool set) { serchMove = set; }
    public bool GetSerchMove() { return serchMove; }
    public void SetisAttack(bool set) { isAttack = set; }
    public void SetIsMove(bool set) { isMove = set; }
    public void AddLevel(int set)
    {
        currentLevel += set;
        //Debug.Log("koyadoLevel=" + currentLevel);
    }

    public void SetYadoNum(int num, bool set) { YadoNum[num] = set; }
    public bool GetYadoNum(int num) { return YadoNum[num]; }

    public void SetTargetObj(GameObject setTargetObj) { targetObj = setTargetObj; }
    public void Damage(int value) { currentHP -= value; }

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        rigidbody = parent.GetComponent<Rigidbody2D>();

        currentHP = hp;

        isMove = true;
        serchMove = false;
        isAttack = false;
        currentLevel = initlevel;
        attckCoolTimeCount = 0;
        currentAttackPower = attackPower[0];
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log("isMove=" + isMove + ",serch=" + serchMove + ",attack=" + isAttack);

        Debug.Log("�R���hHP=" + currentHP);
        if (alive)
        {
            Move();
            SerchMove();
            Attack();
        }
        if (hp < 0)
        {
            alive = false;
        }

        // parent.transform.position=transform.position;

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
        if (targetObj==null) 
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
        if (targetObj.gameObject.tag=="EnemyBody")
        {
            targetObj.GetComponent<EnemyScript>().Damage(currentAttackPower);

        }else if (targetObj.gameObject.tag == "EnemyHokora")
        {
            targetObj.GetComponent<Hokora_enemy>().Damage(currentAttackPower);
        }
        attckCoolTimeCount = attackCoolTime;
        Debug.Log("�G��Damage��^�����I");



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyFightCollision")
        {
            if(!isAttack)
            {
                //�R���C�_�[�̐e�̎q��1�Ԗڂ��^�[�Q�b�g�ɂ���
                targetObj = collision.transform.parent.transform.GetChild(0).gameObject;
                serchMove = false;
                isMove = false;
                isAttack = true;
            }
        }else if (collision.gameObject.tag == "EnemyHokora")
        {
            if (!isAttack)
            {
                //�R���C�_�[�̐e�̎q��1�Ԗڂ��^�[�Q�b�g�ɂ���
                targetObj = collision.gameObject;
                serchMove = false;
                isMove = false;
                isAttack = true;
            }
        }
    }
}

