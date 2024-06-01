using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 moveSpeed;
    Rigidbody2D playerRigidbody;
    private Vector2 input;
    private GameObject playerAnimeObj;
    private Animator playerAnime = null;
    private bool isYadoHold;
    GameObject hitObj;
    GameObject holdObj;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        isYadoHold = false;
        playerAnimeObj = transform.GetChild(0).gameObject;
        playerAnime = playerAnimeObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HoldAndRelese();

        SetAnimator();
    }


    void Move()
    {


        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        Vector2 velocity = input * moveSpeed;
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

    void HoldAndRelese()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (isYadoHold == false )
            {
                if (hitObj != null)
                {
                    if (hitObj.gameObject.tag == "Yado")
                    {
                        holdObj = hitObj;
                        holdObj.GetComponent<YadoScript>().SetIsHold(true);
                        isYadoHold = true;
                        Debug.Log("isYadoHold");
                    }
                }
               
            }
            else
            {
               
                isYadoHold = false;
                holdObj.GetComponent<YadoScript>().SetIsHold(false);
                holdObj.transform.position = new Vector3(transform.position.x,transform.position.y,0);
                Debug.Log("isYadoRelese");
            }
        }


    }

 

    void SetAnimator()
    {
        playerAnime.SetFloat("speed", input.magnitude);
        playerAnime.SetBool("isYadoHold", isYadoHold);


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitObj = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        hitObj = null;
    }
}
