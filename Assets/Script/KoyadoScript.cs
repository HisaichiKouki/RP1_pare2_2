using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class KoyadoScript : MonoBehaviour
{
    GameObject parent;

    [SerializeField, Header("�R���h�̑���")] private float[] speed;
    [SerializeField, Header("�R���h��Hp")] private int[] hp;
    [SerializeField, Header("�R���h�̍U����")] private int[] attackPower;
    [SerializeField, Header("�R���h�̍U���Ԋu")] private float attackCoolTime;
    [SerializeField, Header("�f�o�b�O�p�A�R���h�������x��")] private int initLevel;
    [SerializeField, Header("�R���h�J���̃e�N�X�`��")] private GameObject[] koyadoTex;
    [SerializeField, Header("�R���h�J���̃T�C�Y")] private float[] size;

    [SerializeField] private GameObject serathObj;

    //[SerializeField] GameObject target;
    bool alive = true;

    bool isMove;
    bool serchMove;
    bool isAttack;

    float currentSpeed;
    int currentHP;
    int currentAttackPower;
    float attckCoolTimeCount;

    bool[] YadoNum = new bool[3];


    int currentLevel;
    Vector3 initialSerchScale;

    private Rigidbody2D rigidbody;

    GameObject targetObj;
    Vector2 newVelocity;
    void SetParameter()
    {
        for (int i = 0; i < koyadoTex.Length; i++)
        {
            koyadoTex[i].SetActive(false);
        }
        if (currentLevel >= 9)
        {
            currentSpeed = speed[3];//�����ŗv�f�����߂�
            currentHP = hp[3];
            currentAttackPower = attackPower[3];
            koyadoTex[3].SetActive(true);

            transform.localScale = new Vector3(size[3], size[3], 1);
            serathObj.transform.localScale = initialSerchScale * (size[2] + 1.0f);
            serathObj.transform.localPosition = new Vector3(3.3f, 0, -1.0f);

        }
        else if (currentLevel >= 6)
        {
            currentSpeed = speed[2];//�����ŗv�f�����߂�
            currentHP = hp[2];
            currentAttackPower = attackPower[2];
            koyadoTex[2].SetActive(true);
          
            transform.localScale = new Vector3(size[2], size[2],1);
            serathObj.transform.localScale = initialSerchScale * (size[2] + 1.0f);
            serathObj.transform.localPosition = new Vector3(2.54f, 0, -1.0f);

        }
        else if (currentLevel >= 2)
        {
            currentSpeed = speed[1];//�����ŗv�f�����߂�
            currentHP = hp[1];
            currentAttackPower = attackPower[1];
            koyadoTex[1].SetActive(true);

            transform.localScale = new Vector3(size[1], size[1],1);
            serathObj.transform.localScale = initialSerchScale*(size[1] + 1.0f);
            serathObj.transform.localPosition = new Vector3(1.94f, 0, -1.0f);

        }
        else
        {
            currentSpeed = speed[0];//�����ŗv�f�����߂�
            currentHP = hp[0];
            currentAttackPower = attackPower[0];
            koyadoTex[0].SetActive(true);

            transform.localScale = new Vector3(size[0], size[0], 1);
            //serathObj.transform.localScale = size[0];
            serathObj.transform.localPosition = new Vector3(1.22f, 0, -1.0f);

        }
    }

    //�q���h�̃Z���T�[�����h�A�������͓G����������true�ɂ��A�^�[�Q�b�g�̕����ɂ�����
    //���h���q���h�ɓ�����A�������͓G�Ɛ퓬�ɂȂ�����false��
    public void SetSerchMove(bool set) { serchMove = set; }
    public bool GetSerchMove() { return serchMove; }
    public void SetisAttack(bool set) { isAttack = set; }
    public void SetIsMove(bool set) { isMove = set; }
    public void AddLevel(int set)
    {
        currentLevel += set;
        SetParameter();


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

        currentHP = hp[0];

        isMove = true;
        serchMove = false;
        isAttack = false;
        currentLevel = initLevel;
        initialSerchScale = serathObj.transform.localScale;
        SetParameter();
        attckCoolTimeCount = attackCoolTime;
       
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
        //if (hp < 0)
        //{
        //    alive = false;
        //}

        // parent.transform.position=transform.position;

    }
    void Move()
    {
        if (isAttack || !isMove)
        {
            return;
        }
        rigidbody.velocity = new Vector2(currentSpeed, 0);
    }

    void SerchMove()
    {
        if (!serchMove || targetObj == null) { return; }

        newVelocity = targetObj.transform.position - transform.position;

        rigidbody.velocity = newVelocity.normalized * currentSpeed;

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
        if (targetObj.gameObject.tag=="EnemyBody")
        {
            targetObj.GetComponent<EnemyScript>().Damage(currentAttackPower);

        }else if (targetObj.gameObject.tag == "EnemyHokora")
        {
            targetObj.GetComponent<Hokora_enemy>().Damage(currentAttackPower);
        }
        else if (targetObj.gameObject.tag == "TekiHome")
        {
            targetObj.GetComponent<EnemyHomeScript>().Damage(currentAttackPower);
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
        else if (collision.gameObject.tag == "TekiHome")
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

