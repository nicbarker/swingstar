using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundBehaviour : MonoBehaviour, IPointerDownHandler
{
    public AudioClip onMouseDown;
    public AudioClip onMouseUp;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = onMouseDown;
        audioSource.Play();
    }
}
