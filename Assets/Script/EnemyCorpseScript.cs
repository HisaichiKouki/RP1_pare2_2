using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCorpseScript : MonoBehaviour
{

    [SerializeField, Header("消滅タイマー")] private float deleteTime;
    private float isDeleteCount;


    // Start is called before the first frame update
    void Start()
    {
        isDeleteCount = deleteTime;

    }

    // Update is called once per frame
    void Update()
    {
        DeleteCorpse();
    }

    void DeleteCorpse()
    {
        if (isDeleteCount > 0)
        {
            isDeleteCount -= Time.deltaTime;
            return;
        }

        Destroy(this.gameObject);
    }





}
