using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    [SerializeField, Header("スポーンするオブジェクト")] private GameObject spownPrefab;
    [SerializeField, Header("スポーン間隔")] private float spownTime;
    [SerializeField, Header("HP")] private int hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
