using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextScript : MonoBehaviour
{
    private Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText = GetComponent<Text>();
        levelText.text = "ClearTime:" + EnemyHomeScript.gameTime.ToString("N2");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
