using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class KoyadoScript : MonoBehaviour
{
    GameObject parent;

    [SerializeField, Header("コヤドの速さ")] private float speed;
    [SerializeField, Header("コヤドのHp")] private int hp;
    [SerializeField, Header("コヤドの攻撃力")] private int[] attackPower;
    [SerializeField, Header("コヤドの攻撃感覚")] private float attackCoolTime;

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


    //子ヤドのセンサーがヤド、もしくは敵を見つけたらtrueにし、ターゲットの方向にすすむ
    //ヤドが子ヤドに当たる、もしくは敵と戦闘になったらfalseに
    public void SetSerchMove(bool set) { serchMove = set; }
    public bool GetSerchMove() { return serchMove; }
    public void SetisAttack(bool set) { isAttack = set; }
    public void SetIsMove(bool set) { isMove = set; }
    public void AddLevel(int set)
    {
        currentLevel += set;
        Debug.Log("koyadoLevel=" + currentLevel);
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
        currentLevel = 1;
        attckCoolTimeCount = 0;
        currentAttackPower = attackPower[0];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isMove=" + isMove + ",serch=" + serchMove + ",attack=" + isAttack);

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

        //自分のHPが無くなった時
        if (currentHP <= 0)
        {
            Destroy(parent.gameObject);
            return;
        }
        //相手を倒した時
        if (targetObj==null) 
        {
            attckCoolTimeCount = 0;
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
        targetObj.GetComponent<EnemyScript>().Damage(currentAttackPower);
        attckCoolTimeCount = attackCoolTime;
        Debug.Log("Damageを与えた！");



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
                //コライダーの親の子の1番目をターゲットにする
                targetObj = collision.transform.parent.transform.GetChild(0).gameObject;
                serchMove = false;
                isMove = false;
                isAttack = true;
            }
           

        }
    }
}

