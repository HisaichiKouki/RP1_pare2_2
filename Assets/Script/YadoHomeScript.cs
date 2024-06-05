using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YadoHomeScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleprefab;

    [SerializeField] private int hitpoint;
    private int isHitPoint;
    // Start is called before the first frame update
    public GameObject hitPointBar;
    HitPointBarScript hitPointBarScript;


    void Start()
    {
        isHitPoint = hitpoint;
        hitPointBarScript = hitPointBar.GetComponent<HitPointBarScript>();
        hitPointBarScript.hitPoint = isHitPoint;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int value)
    {
        isHitPoint -= value;
        //Debug.Log("isHitPoint=" + isHitPoint);
        hitPointBarScript.hitPoint = isHitPoint;
        particleprefab.Play();
        if (isHitPoint <= 0)
        {
            //ゲームオーバー
            Debug.Log("<color=red>GameOver</color>");
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
