using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField, Header("�G��HP")] private int hp;
    [SerializeField, Header("�G�̈ړ����x")] private float spped;
    [SerializeField, Header("�G�̍U����")] private int attackPower;

    GameObject parent;

    bool isAttack;
    int currentHP;
    GameObject targetObj;
    public void SetTargetObj(GameObject setTargetObj) { targetObj = setTargetObj; }

    public void Damage(int value) { currentHP -= value; }
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        currentHP = hp;
        isAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Debug.Log("enemyHP="+currentHP);
    }

    void Attack()
    {
        if (!isAttack) { return; }
        if (currentHP <= 0)
        {
            Destroy(parent.gameObject);
        }
    }
}
