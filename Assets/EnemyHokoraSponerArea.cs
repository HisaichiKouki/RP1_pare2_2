using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHokoraSponerArea : MonoBehaviour
{
    [SerializeField, Header("スポーンするオブジェクト")] private GameObject spownPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject spownObj = Instantiate(spownPrefab);
            Destroy(this.gameObject);
        }
    }
    
}
