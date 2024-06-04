using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextScript : MonoBehaviour
{

    public GameObject parentObj;
    KoyadoScript koyadoScript;
    private Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        koyadoScript= parentObj.GetComponent<KoyadoScript>();
        levelText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text="LEVEL"+ koyadoScript.GetCurrentLevel().ToString("");
    }
}
