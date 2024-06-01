using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YadoScript : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject childObj;
    private bool isHold;
    public float[] levelUpTime;
    private float xpCount;
    private int levelCount;//���݂̃��x��

    public void SetIsHold(bool set) { isHold = set; }
    void Start()
    {
        childObj = transform.GetChild(0).gameObject;
        levelCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold)
        {
            if (levelCount != levelUpTime.Length) {
                xpCount += Time.deltaTime;
                //���h�̌o���l�����x���敪�̗ʂɒB�����烌�x���A�b�v����
                if (xpCount >= levelUpTime[levelCount])
                {
                    xpCount = 0;
                    levelCount++;
                    Debug.Log("nowLevel=" + levelCount);
                }
            }
            

           


            childObj.SetActive(false);
        }
        else
        {
            childObj.SetActive(true);
        }
    }
}
