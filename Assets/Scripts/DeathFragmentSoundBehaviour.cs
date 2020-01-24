using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFragmentSoundBehaviour : MonoBehaviour
{
    private AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        deathSound = Resources.Load<AudioClip>("Audio/Death/glass_small");
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = deathSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 5)
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.pitch = 1 + Random.Range(-0.1f, 0.1f);
            audioSource.Play();
        }
    }
}
