using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOutline : MonoBehaviour
{
    [SerializeField, Header("�A�E�g���C���I�u�W�F�N�g")] public GameObject outline;

    public void Draw()
    {
        outline.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        outline.gameObject.SetActive(false);
    }
}
