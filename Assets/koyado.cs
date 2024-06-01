using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class koyado : MonoBehaviour
{
    [SerializeField] GameObject target;
    GameObject hokora;
    Vector3 vec;
    bool alive = true;

    int hp;


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
        vec.x += 0.01f;
        transform.position = vec;
    }
}

