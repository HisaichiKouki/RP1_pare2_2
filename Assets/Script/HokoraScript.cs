using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HokoraScript : MonoBehaviour
{
    [SerializeField, Header("スポーンするオブジェクト")] private GameObject spownPrefab;
    [SerializeField, Header("スポーン間隔")] private float spownTime;
    [SerializeField, Header("祠のHP")] private int hitPoint;

    [SerializeField] private float respownTime;
    float respownCount;
    [SerializeField, Header("リスポーンする地点")] private Vector2 respownPos;

    private float spownCount;
    private bool isHold;

    int isHitPoint;
    bool isBroken;

    GameObject childObj;
    private CapsuleCollider2D collider;
    PlayerScript playerScript;

    public bool GetIsBroken() { return isBroken; }

    public void SetIsHold(bool set) { isHold = set; }
    // Start is called before the first frame update
    void Start()
    {
        spownCount = 0;
        isHold = false;
        childObj = transform.GetChild(0).gameObject;
        collider = GetComponent<CapsuleCollider2D>();
        playerScript = FindAnyObjectByType<PlayerScript>();
        isHitPoint = hitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBroken)
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
        else
        {
            Respown();
        }
    }
    public void Damage(int value)
    {

        isHitPoint -= value;
        Debug.Log("isHitPoint=" + isHitPoint);

        if (isHitPoint <= 0)
        {

            isHitPoint = hitPoint;
            isBroken = true;

            Debug.Log("isBroken");
            collider.enabled = false;
            childObj.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    void Spown()
    {
        if (spownCount > 0)
        {
            spownCount -= Time.deltaTime;
            return;
        }

        if (spownPrefab == null || isHold) { return; }
        GameObject spownObj = Instantiate(spownPrefab);
        Vector3 newPosition = transform.position;
        newPosition.x += 0.5f;
        newPosition.y -= 0.4f;
        newPosition.z = -1;
        spownObj.transform.position = newPosition;
        spownCount = spownTime;
    }

    void Respown()
    {
        if (!isBroken) { return; }
        respownCount += Time.deltaTime;
        if (respownCount >= respownTime)
        {
            transform.position = respownPos;
            isBroken = false;
            respownCount = 0;
            isHitPoint = hitPoint;
            collider.enabled = true;
            childObj.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
