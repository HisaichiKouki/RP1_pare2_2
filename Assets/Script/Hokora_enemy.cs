using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hokora_enemy : MonoBehaviour
{

    [SerializeField, Header("スポーンするオブジェクト")] private GameObject spownPrefab;
    [SerializeField, Header("スポーン間隔")] private float spownTime;
    [SerializeField, Header("祠のHP")] private int hitPoint;

    int isHitPoint;
    private float spownCount;

    // Start is called before the first frame update
    void Start()
    {
        isHitPoint = hitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Spown();
    }

    public void Damage(int value)
    {

        isHitPoint -= value;
        Debug.Log("テキホコラisHitPoint=" + isHitPoint);

        if (isHitPoint <= 0)
        {

            isHitPoint = hitPoint;
            Destroy(this.gameObject);

            Debug.Log("isBroken");
           
        }
    }
    void Spown()
    {
        if (spownCount > 0)
        {
            spownCount -= Time.deltaTime;
            return;
        }

        if (spownPrefab == null ) { return; }
        GameObject spownObj = Instantiate(spownPrefab);
        Vector3 newPosition = transform.position;
        newPosition.x -= 0.5f;
        newPosition.y -= 0.4f;
        newPosition.z = -1;
        spownObj.transform.position = newPosition;
        spownCount = spownTime;
    }
}
