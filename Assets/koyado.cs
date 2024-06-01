using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class koyado : MonoBehaviour
{
    [SerializeField, Header("コヤドの速さ")] private float speed;
    [SerializeField, Header("コヤドのHp")] private int hp;
    [SerializeField] GameObject target;
    GameObject hokora;
    Vector3 vec;
    bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Hokora");
        vec = target.transform.position;
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
        vec.x += speed;
        transform.position = vec;
    }
}

