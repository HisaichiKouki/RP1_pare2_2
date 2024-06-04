using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointShoutScript : MonoBehaviour
{

    [Header("�ϋq�̐��������ŊǗ�����")]
    public GameObject canvas;//�L�����o�X
    [SerializeField, Header("�N�[���^�C��")] private float setCoolTime;
    [SerializeField, Header("�ǂ�ȃ^�C�v�̃e�L�X�g��")] private GameObject shoutObj;

    [SerializeField, Header("�^�C�v���Ƃ̃e�L�X�g�̓��e")] private string[] shoutTexts1;
    [SerializeField] private string[] shoutTexts2;
    [SerializeField] private string[] shoutTexts3;


    [SerializeField] private string[] shoutKoyadoLevelUP;


    private bool[] isShout=new bool[3];
    private bool koyadoLevelUp;

    float coolTimeCount;
    float koyadoCoolTimeCount;

    
    public void setShout(int value) { isShout[value] = true; }
    public void setKoyadoLevelUpShout() { koyadoLevelUp = true; }
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        coolTimeCount = 0;
        koyadoLevelUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        CoolTime();
        KoyadoCoolTime();
        if (koyadoCoolTimeCount<=0)
        {
            if (koyadoLevelUp)
            {
                if (shoutKoyadoLevelUP.Length > 0)
                {
                    //�����ŗ����𐶐�
                    int rnd = UnityEngine.Random.Range(0, shoutKoyadoLevelUP.Length);
                    //�v���n�u�̃^�C�v�ɂ���ďo�����ς��
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //�\�����錾�t�𗐐��Ō��߂�
                    text.GetComponent<Text>().text = shoutKoyadoLevelUP[rnd];
                    //�L�����o�X�����߂Ȃ��ƕ`�悳��Ȃ�
                    text.transform.SetParent(canvas.transform, false);
                    //�����T�C�Y���O��
                    text.transform.localScale = Vector3.zero;
                    koyadoLevelUp = false;
                    koyadoCoolTimeCount = setCoolTime*5;
                }
            }

        }
        if (coolTimeCount<=0)
        {
            if (isShout[0])
            {
                if (shoutTexts1.Length > 0)
                {
                    //�����ŗ����𐶐�
                    int rnd = UnityEngine.Random.Range(0, shoutTexts1.Length);
                    //�v���n�u�̃^�C�v�ɂ���ďo�����ς��
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //�\�����錾�t�𗐐��Ō��߂�
                    text.GetComponent<Text>().text = shoutTexts1[rnd];
                    //�L�����o�X�����߂Ȃ��ƕ`�悳��Ȃ�
                    text.transform.SetParent(canvas.transform, false);
                    //�����T�C�Y���O��
                    text.transform.localScale = Vector3.zero;
                    isShout[0] = false;
                    coolTimeCount = setCoolTime;
                }
            }
            else if (isShout[1])
            {
                if (shoutTexts2.Length > 0)
                {
                    //�����ŗ����𐶐�
                    int rnd = UnityEngine.Random.Range(0, shoutTexts2.Length);
                    //�v���n�u�̃^�C�v�ɂ���ďo�����ς��
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //�\�����錾�t�𗐐��Ō��߂�
                    text.GetComponent<Text>().text = shoutTexts2[rnd];
                    //�L�����o�X�����߂Ȃ��ƕ`�悳��Ȃ�
                    text.transform.SetParent(canvas.transform, false);
                    //�����T�C�Y���O��
                    text.transform.localScale = Vector3.zero;
                    isShout[1] = false;
                    coolTimeCount = setCoolTime;
                }
            }
            else if (isShout[2])
            {
                if (shoutTexts3.Length > 0)
                {
                    //�����ŗ����𐶐�
                    int rnd = UnityEngine.Random.Range(0, shoutTexts3.Length);
                    //�v���n�u�̃^�C�v�ɂ���ďo�����ς��
                    GameObject text = Instantiate(shoutObj);
                    text.gameObject.SetActive(true);
                    //�\�����錾�t�𗐐��Ō��߂�
                    text.GetComponent<Text>().text = shoutTexts3[rnd];
                    //�L�����o�X�����߂Ȃ��ƕ`�悳��Ȃ�
                    text.transform.SetParent(canvas.transform, false);
                    //�����T�C�Y���O��
                    text.transform.localScale = Vector3.zero;
                    isShout[2] = false;
                    coolTimeCount = setCoolTime;
                }
            }

        }
        
    }

    void CoolTime()
    {
        if (coolTimeCount <= 0) return;
        coolTimeCount -= Time.deltaTime;
    }

    void KoyadoCoolTime()
    {
        if (koyadoCoolTimeCount <= 0) return;
        koyadoCoolTimeCount -= Time.deltaTime;
    }
}
