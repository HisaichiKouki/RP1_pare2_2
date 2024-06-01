using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoutManager : MonoBehaviour
{
    [Header("�ϋq�̐��������ŊǗ�����")]
    public GameObject canvas;//�L�����o�X
    [SerializeField, Header("�ǂ�ȃ^�C�v�̃e�L�X�g��")] private GameObject[] shoutObj;

    [SerializeField,Header("�^�C�v���Ƃ̃e�L�X�g�̓��e")] private string[] shoutTexts1;
    [SerializeField] private string[] shoutTexts2;
    [SerializeField] private string[] shoutTexts3;

    bool shoutEnabled;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        shoutEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            //�����ŗ����𐶐�
            int rnd = UnityEngine.Random.Range(0, shoutTexts1.Length);
            //�v���n�u�̃^�C�v�ɂ���ďo�����ς��
            GameObject text = Instantiate(shoutObj[0]);
            text.gameObject.SetActive(true);
            //�\�����錾�t�𗐐��Ō��߂�
            text.GetComponent<Text>().text = shoutTexts1[rnd];
            //�L�����o�X�����߂Ȃ��ƕ`�悳��Ȃ�
            text.transform.SetParent(canvas.transform, false);
            //�����T�C�Y���O��
            text.transform.localScale = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            int rnd = UnityEngine.Random.Range(0, shoutTexts2.Length);
            GameObject text = Instantiate(shoutObj[1]);
            text.gameObject.SetActive(true);
            text.GetComponent<Text>().text = shoutTexts2[rnd];
            text.transform.SetParent(canvas.transform, false);
            text.transform.localScale = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            int rnd=UnityEngine.Random.Range(0, shoutTexts3.Length);
            GameObject text = Instantiate(shoutObj[2]);
            text.gameObject.SetActive(true);
            text.GetComponent<Text>().text = shoutTexts3[rnd];
            text.transform.SetParent(canvas.transform, false);
            text.transform.localScale = Vector3.zero;
        }
    }
}
