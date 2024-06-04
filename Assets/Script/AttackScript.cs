using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    private int attackPower;
    private float attackTime;

    BossScript bossScript;
    
    public void SetAttackPower(int setPower) {  attackPower = setPower; }
    public void SetAttackTime(float setTime) { attackTime = setTime; }
    // Start is called before the first frame update
    void Start()
    {
        bossScript=FindAnyObjectByType<BossScript>();   
    }

    // Update is called once per frame
    void Update()
    {
        attackTime-=Time.deltaTime;
        if (attackTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "koyado")
        {
            //if (collision.gameObject.GetComponent<KoyadoScript>().GetIsHitPoint() - attackPower <= 0)
            //{
            //    bossScript.MinasTargetCount();
            //}
            collision.gameObject.GetComponent<KoyadoScript>().Damage(attackPower);
            
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
