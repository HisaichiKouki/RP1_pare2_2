using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YadoUIScript : MonoBehaviour
{
    PlayerScript playerScript;
    [SerializeField, Header("h‚ğ‚Á‚Ä‚È‚¢‚ÌUI")] private GameObject notHoldUI;
    [SerializeField, Header("h‚ğ‚Á‚Ä‚é‚ÌUI")] private GameObject isHold;
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
