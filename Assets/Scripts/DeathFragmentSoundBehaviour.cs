using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFragmentSoundBehaviour : MonoBehaviour
{
    private AudioClip deathSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        var random = (int)Random.Range(1, 4);
        deathSound = Resources.Load<AudioClip>("Audio/Death/GlassSmash" + random.ToString());
        audioSource = gameObject.AddComponent<AudioSource>();
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
            audioSource.pitch = 1 + Random.Range(-0.1f, 0.1f);
            audioSource.Play();
        }
    }
}
