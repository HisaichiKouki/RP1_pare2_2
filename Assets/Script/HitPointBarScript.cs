using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointBarScript : MonoBehaviour
{

    [SerializeField,Header("â°ÇÃí∑Ç≥åWêî")]private float scaleX;
    public int hitPoint;
    Vector3 newScale;
    //GameObject parentObj;
    
    public void SetLocalScale( float value)
    {
        newScale.y = value;

    }
    // Start is called before the first frame update
    void Start()
    {
        newScale=transform.localScale;
        //parentObj = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        newScale.x = hitPoint * scaleX;
        transform.localScale = newScale;
    }
}
