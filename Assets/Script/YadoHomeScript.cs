using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YadoHomeScript : MonoBehaviour
{

    [SerializeField] private int hitpoint;
    private int isHitPoint; 
    // Start is called before the first frame update


    void Start()
    {
        isHitPoint = hitpoint;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int value)
    {
        isHitPoint -= value;
        //Debug.Log("isHitPoint=" + isHitPoint);

        if (isHitPoint <= 0)
        {
            //�Q�[���I�[�o�[
            Debug.Log("<color=red>GameOver</color>");
        }
    }
}