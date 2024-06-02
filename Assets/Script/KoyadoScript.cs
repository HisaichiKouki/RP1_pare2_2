using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class KoyadoScript : MonoBehaviour
{
    [SerializeField, Header("コヤドの速さ")] private float speed;
    [SerializeField, Header("コヤドのHp")] private int hp;

    //[SerializeField] GameObject target;
    bool alive = true;

    bool isMove;
    bool serchMove;
    bool attack;

    bool[] YadoNum= new bool[3];

    int currentLevel;

    private Rigidbody2D rigidbody;

    GameObject moveTargetObj;
    Vector2 newVelocity;


    //子ヤドのセンサーがヤド、もしくは敵を見つけたらtrueにし、ターゲットの方向にすすむ
    //ヤドが子ヤドに当たる、もしくは敵と戦闘になったらfalseに
    public void SetSerchMove(bool set) { serchMove = set; }
    public bool GetSerchMove() { return serchMove; }
    public void SetAttack(bool set) { attack = set; }
    public void SetIsMove(bool set) { isMove = set; }
    public void AddLevel(int set) 
    { 
        currentLevel += set;
        Debug.Log("koyadoLevel=" + currentLevel);
    }

    public void SetYadoNum(int num,bool set) { YadoNum[num] = set; }
    public bool GetYadoNum(int num) { return YadoNum[num]; }

    public void SetMoveTargetObj(GameObject targetObj) {  moveTargetObj = targetObj; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
       
        hp = 100;

        isMove = true;
        serchMove = false;
        attack = false;
        currentLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            Move();
            SerchMove();
        }
        if (hp < 0)
        {
            alive = false;
        }

    }
    void Move()
    {
        if (attack||!isMove)
        {
            return;            
        }
        rigidbody.velocity = new Vector2(speed,0);
    }

    void SerchMove()
    {
        if (!serchMove || moveTargetObj == null) {  return; }

        newVelocity = moveTargetObj.transform.position - transform.position;
       
        rigidbody.velocity = newVelocity.normalized * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}

