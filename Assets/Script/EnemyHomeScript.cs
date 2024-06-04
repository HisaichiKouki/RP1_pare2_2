using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomeScript : MonoBehaviour
{

    [SerializeField] private int hitpoint;
    private int isHitPoint;
    public GameObject hitPointBar;
    HitPointBarScript hitPointBarScript;

    // Start is called before the first frame update
    void Start()
    {
        isHitPoint = hitpoint;
        hitPointBarScript = hitPointBar.GetComponent<HitPointBarScript>();
        hitPointBarScript.hitPoint = isHitPoint;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int value)
    {
        isHitPoint -= value;
        if (isHitPoint <= 0)
        {
            isHitPoint = 0;
            //Debug.Log("<color=cyan>GameClear</color>");

        }
        hitPointBarScript.hitPoint = isHitPoint;

        //Debug.Log("isHitPoint=" + isHitPoint);

        
    }
}
