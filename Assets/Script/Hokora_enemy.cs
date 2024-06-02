using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hokora_enemy : MonoBehaviour
{

    [SerializeField, Header("�X�|�[������I�u�W�F�N�g")] private GameObject spownPrefab;
    [SerializeField, Header("�X�|�[���Ԋu")] private float spownTime;
    [SerializeField, Header("�K��HP")] private int hitPoint;

    int isHitPoint;
    private float spownCount;

    // Start is called before the first frame update
    void Start()
    {
        isHitPoint = hitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Spown();
    }

    public void Damage(int value)
    {

        isHitPoint -= value;
        Debug.Log("�e�L�z�R��isHitPoint=" + isHitPoint);

        if (isHitPoint <= 0)
        {

            isHitPoint = hitPoint;
            Destroy(this.gameObject);

            Debug.Log("isBroken");
           
        }
    }
    void Spown()
    {
        if (spownCount > 0)
        {
            spownCount -= Time.deltaTime;
            return;
        }

        if (spownPrefab == null ) { return; }
        GameObject spownObj = Instantiate(spownPrefab);
        Vector3 newPosition = transform.position;
        newPosition.x -= 0.5f;
        newPosition.y -= 0.4f;
        newPosition.z = -1;
        spownObj.transform.position = newPosition;
        spownCount = spownTime;
    }
}
