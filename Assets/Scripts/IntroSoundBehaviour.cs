using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroSoundBehaviour : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isLoadingNextScene;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && !isLoadingNextScene) {
            isLoadingNextScene = true;
            GameObject.Find("FadeOutPanel").GetComponent<FadeOutBehaviour>().StartFade(() => SceneManager.LoadScene("MainMenu"));
        }
    }
}
