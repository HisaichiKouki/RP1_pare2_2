using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpowner : MonoBehaviour
{
    [SerializeField, Header("çUåÇä‘äu")] private float setCoolTime;
    [SerializeField, Header("É{ÉXÇÃçUåÇóÕ")] private int attackPower;

    float coolTimeCount;
    public GameObject attackPrefab;
    // Start is called before the first frame update
    void Start()
    {
        coolTimeCount = setCoolTime;
    }

    // Update is called once per frame
    void Update()
    {

        coolTimeCount -= Time.deltaTime;
        if (coolTimeCount < 0)
        {
            GameObject spownObj=Instantiate(attackPrefab);
            spownObj.transform.position = transform.position;
            spownObj.GetComponent<AttackScript>().SetAttackPower(attackPower);
            spownObj.GetComponent<AttackScript>().SetAttackTime(1);
            coolTimeCount = setCoolTime;
        }
    }
}
