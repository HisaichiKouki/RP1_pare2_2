using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HokoraScript : MonoBehaviour
{
    [SerializeField, Header("スポーンするオブジェクト")] private GameObject spownPrefab;
    [SerializeField, Header("スポーン間隔")] private float spownTime;
    [SerializeField, Header("祠のHP")] private int hitPoint;
    private float spownCount;
    private bool isHold;

    GameObject childObj;
    private CapsuleCollider2D collider;
    PlayerScript playerScript;


    public void SetIsHold(bool set) {  isHold = set; }
    // Start is called before the first frame update
    void Start()
    {
        spownCount = 0;
        isHold = false;
        childObj = transform.GetChild(0).gameObject;
        collider = GetComponent<CapsuleCollider2D>();
        playerScript = FindAnyObjectByType<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetIsHold())
        {
            collider.isTrigger = false;
        }
        else
        {
            collider.isTrigger = true;
        }

        if (isHold)
        {
            childObj.SetActive(false); 
            collider.enabled = false;
        }
        else 
        {
            Spown();

            childObj.SetActive(true);
            collider.enabled = true;

           
        }
    }

    void Spown()
    {
        if (spownCount > 0)
        {
            spownCount-=Time.deltaTime;
            return;
        }

        if( spownPrefab == null|| isHold) { return; }
        GameObject  spownObj=Instantiate(spownPrefab);
        spownObj.transform.position=transform.position;
        spownCount = spownTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
