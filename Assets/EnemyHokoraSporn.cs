using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHokoraSporn : MonoBehaviour
{
    [SerializeField, Header("�X�|�[������I�u�W�F�N�g")] private GameObject spownPrefab;
    [SerializeField, Header("�X�|�[������(�b)")] private float spownTime;

    float time;
    bool spown;
    // Start is called before the first frame update
    void Start()
    {
        time = spownTime;
        spown = false;
    }

    // Update is called once per frame
    void Update()
    {
        spownTime -= Time.deltaTime;
        if(spownTime <= 0&& !spown)
        {
            
            GameObject spownObj = Instantiate(spownPrefab);
            spownObj.transform.position = transform.position;
            spown = true;
        }
    }
}
