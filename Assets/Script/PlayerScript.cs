using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class PlayerScript : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private ParticleSystem particleprefab;

    [SerializeField] private Vector2 moveSpeed;
    private Vector2 nowMoveSpeed;
    Rigidbody2D playerRigidbody;
    private Vector2 input;
    private GameObject playerAnimeObj;
    private Animator playerAnime = null;
    private bool isHold;
    private bool isRelese;
    private bool isYadoHold;//持ったものごとに特別な処理をする場合のフラグ
    private bool isHokoraHold;
    GameObject hitObj;
    GameObject holdObj;

    private CapsuleCollider2D capsuleCollider;

    public bool GetIsHold() { return isHold; }
    // Start is called before the first frame update
    void Start()
    {
        audioSource=gameObject.GetComponent<AudioSource>();
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        isYadoHold = false;
        isHokoraHold = false;
        playerAnimeObj = transform.GetChild(0).gameObject;
        playerAnime = playerAnimeObj.GetComponent<Animator>();
        nowMoveSpeed = moveSpeed;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Hold();
        Relese();
        

        SetAnimator();


    }


    void Move()
    {


        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        Vector2 velocity = input * nowMoveSpeed;
        playerRigidbody.velocity = velocity;
        if (velocity.x > 0)
        {
            playerAnimeObj.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (velocity.x < 0)
        {
            playerAnimeObj.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Hold()
    {
        isRelese = isHold;

        //既に持っていたらリターン
        if (isHold && isRelese) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            if (hitObj != null)
            {
                hitObj.transform.GetChild(1).gameObject.SetActive(false);
                capsuleCollider.offset = new Vector2(0, 0);
                capsuleCollider.size = new Vector2(1, 1.2f);
                isHold = true;
                particleprefab.Play();
                //ここにオブジェクトごとの関数をいれる
                YadoHold();
                HokoraHold();
                audioSource.Play();
            }
        }
    }
    void Relese()
    {
        //何も持っていなかったらリターン
        if (!isRelese) { return; }
        if (Input.GetButtonDown("Jump"))
        {
            capsuleCollider.offset = new Vector2(-0.03f, -0.23f);
            capsuleCollider.size = new Vector2(1, 0.5f);
            isHold = false;
            particleprefab.Stop();
            YadoRelese();
            HokoraRelese();
            audioSource.Play();
        }
    }

    void YadoHold()
    {
        if (hitObj.gameObject.tag != "Yado1"&& hitObj.gameObject.tag != "Yado2" && hitObj.gameObject.tag != "Yado3") { return; }
        isYadoHold = true;
        holdObj = hitObj;
        holdObj.GetComponent<YadoScript>().SetIsHold(true);
        holdObj.transform.GetChild(2).gameObject.SetActive(false);
        //holdObj.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        Debug.Log("isYadoHold");

    }
    void YadoRelese()
    {
        //ヤドを持っていなかったらリターン
        if (!isYadoHold) { return; }
        isYadoHold = false;
        holdObj.GetComponent<YadoScript>().SetIsHold(false);
        holdObj.transform.GetChild(2).gameObject.SetActive(true);

        holdObj.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Debug.Log("isYadoRelese");
    }
    void HokoraHold()
    {
        if (hitObj.gameObject.tag != "Hokora") { return; }
        //ホコラを持ったら速度を遅くする
        nowMoveSpeed = moveSpeed / 2;

        isHokoraHold = true;
        holdObj = hitObj;
        holdObj.GetComponent<HokoraScript>().SetIsHold(true);
        Debug.Log("isHokoraHold");

    }
    void HokoraRelese()
    {
        //ホコラを持っていなかったらリターン
        if (!isHokoraHold) { return; }
        //速度をもとに戻す
        nowMoveSpeed = moveSpeed;

        isHokoraHold = false;
        holdObj.GetComponent<HokoraScript>().SetIsHold(false);
        holdObj.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Debug.Log("isHokoraRelese");
    }

    void SetAnimator()
    {
        playerAnime.SetFloat("speed", input.magnitude);
        playerAnime.SetBool("isYadoHold", isYadoHold);
        playerAnime.SetBool("isHokoraHold", isHokoraHold);


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyCorpse")
        {
           

            if (isYadoHold)
            {
                Destroy(collision.gameObject);
                holdObj.gameObject.GetComponent<YadoScript>().AddXP();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Yado1" || collision.gameObject.tag == "Yado2" || collision.gameObject.tag == "Yado3" ||
            collision.gameObject.tag == "Hokora")
        {
            //元のオブジェクトの表示を消した後、新しい方を表示する
            if (hitObj != null) { hitObj.transform.GetChild(1).gameObject.SetActive(false); }
            hitObj = collision.gameObject;
            hitObj.transform.GetChild(1).gameObject.SetActive(true);
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Yado1" || collision.gameObject.tag == "Yado2" || collision.gameObject.tag == "Yado3" ||
            collision.gameObject.tag == "Hokora")
        {
            //オブジェクトから離れたらfalseにする
            hitObj.transform.GetChild(1).gameObject.SetActive(false);
            hitObj = null;
        }
    }
}
