using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] private GameObject targetObj;

    [SerializeField, Header("�J������������")] private float leftLimit; 
    [SerializeField, Header("�J�����E������")] private float rightLimit; 
    Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        TargetFollow();
    }

    // Update is called once per frame
    void Update()
    {
        TargetFollow();
    }

    void TargetFollow()
    {
        if (targetObj == null) { return; }
        newPosition = targetObj.transform.position;
        newPosition.y = 0;
        newPosition.z = -10;
        newPosition.x=Mathf.Clamp(newPosition.x, leftLimit, rightLimit);
        transform.position = newPosition;
    }
}
