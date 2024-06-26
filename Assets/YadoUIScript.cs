using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YadoUIScript : MonoBehaviour
{
    PlayerScript playerScript;
    [SerializeField, Header("宿を持ってない時のUI")] private GameObject notHoldUI;
    [SerializeField, Header("宿を持ってる時のUI")] private GameObject isHold;
    // Start is called before the first frame update
    void Start()
    {
        playerScript=FindAnyObjectByType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetIsHold())
        {
            notHoldUI.SetActive(false);
            isHold.SetActive(true);
        }else
        {
            notHoldUI.SetActive(true);
            isHold.SetActive(false);
        }
    }
}
