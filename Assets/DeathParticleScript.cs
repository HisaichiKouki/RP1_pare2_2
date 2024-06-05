using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticleprefab;

    float time;
    // Start is called before the first frame update
    void Start()
    {

        deathParticleprefab.Play();
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        if (time > 2) { Destroy(this.gameObject); }
    }
}
