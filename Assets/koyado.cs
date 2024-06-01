using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class koyado : MonoBehaviour
{
    [SerializeField, Header("コヤドの速さ")] private float speed;
    [SerializeField, Header("コヤドのHp")] private int hp;
    //[SerializeField] GameObject target;
    GameObject hokora;
    Vector3 newPositon;
    bool alive = true;

    private Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
        hokora = GameObject.Find("Hokora");
        newPositon = hokora.transform.position;
        newPositon.x += 0.5f;
        newPositon.y -= 0.4f;
        newPositon.z = -1;
        transform.position = newPositon;
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            Move();
        }
        if (hp < 0)
        {
            alive = false;
        }

    }
    void Move()
    {
        rigidbody.velocity = new Vector2(speed,0);
    }
}

