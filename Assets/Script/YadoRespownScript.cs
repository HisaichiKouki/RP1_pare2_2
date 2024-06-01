using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YadoRespownScript : MonoBehaviour
{

    [SerializeField, Header("リスポーンさせるヤド")] GameObject respownPrefab;
    [SerializeField] private float respownTime;
    GameObject targetObj;
    float respownCount;
    bool isBroken;

    public void IsBroken() { isBroken = true; }
    void Brake()
    {
        if (!isBroken) { return; }
        respownCount += Time.deltaTime;
        if (respownCount >= respownTime)
        {
            GameObject respownObj=Instantiate(respownPrefab);
            respownObj.transform.position = transform.position;
            isBroken = false;
            respownCount = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.FindWithTag("Yado");
        isBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBroken&& targetObj!=null) { isBroken= targetObj.GetComponent<YadoScript>().GetIsBroken(); }
        Brake();
    }
}
