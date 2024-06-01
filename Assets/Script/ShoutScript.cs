using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoutScript : MonoBehaviour
{
    [Header("à–¾")]
    [SerializeField, TextArea] string setumei;
    public float easeTime;
    float easeT;
    float easeSize;
    // Start is called before the first frame update
    void Start()
    {
        easeT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) { easeT = 0; }

        easeT += Time.deltaTime;
        
        easeSize=OutQuintZero(easeT, easeTime, 0, 1, 40);

        transform.localScale = new Vector2(easeSize, easeSize);

        if (easeT > easeTime) { Destroy(this.gameObject); }
    }

    float InQuint(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        return min + deltaValue * t * t * t * t * t;
    }

    float OutQuint(float t, float totaltime, float min, float max)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return max;

        t /= totaltime;
        float deltaValue = max - min;

        t -= 1.0f;
        return min + deltaValue * (1.0f + t * t * t * t * t);
    }
    float OutQuintZero(float t, float totaltime, float min, float max, float backRaito)
    {
        if (t <= 0.0f) return min;
        if (t >= totaltime) return min;
        float backPoint = totaltime * backRaito / 100.0f;


        if (t < backPoint)
        {
            return OutQuint(t, backPoint, min, max);
        }
        else
        {
            return InQuint(t - backPoint, totaltime - backPoint, max, min);
        }
    }
}
