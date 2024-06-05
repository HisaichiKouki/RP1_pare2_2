using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHomeScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleprefab;

    [SerializeField] private int hitpoint;
    private int isHitPoint;
    public GameObject hitPointBar;
    HitPointBarScript hitPointBarScript;

    static public float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        isHitPoint = hitpoint;
        hitPointBarScript = hitPointBar.GetComponent<HitPointBarScript>();
        hitPointBarScript.hitPoint = isHitPoint;
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitPoint > 0) { gameTime += Time.deltaTime; }
    }

    public void Damage(int value)
    {
        isHitPoint -= value;
        particleprefab.Play();
        if (isHitPoint <= 0)
        {
            isHitPoint = 0;
            //Debug.Log("<color=cyan>GameClear</color>");
            SceneManager.LoadScene("GameClearScene");

        }
        hitPointBarScript.hitPoint = isHitPoint;

        //Debug.Log("isHitPoint=" + isHitPoint);

        
    }
}
