using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutBehaviour : MonoBehaviour
{
    public bool initiallyFaded;
    private bool startedFade;
    private float countDown = 1;
    private Action onComplete;
    // Start is called before the first frame update
    void Start()
    {
        var newValue = initiallyFaded ? 1 : 0;
        GetComponent<Image>().color = new Color(0, 0, 0, newValue);
        if (initiallyFaded)
        {
            startedFade = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startedFade)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime * 2;
                var newValue = initiallyFaded ? countDown : 1 - countDown;
                GetComponent<Image>().color = new Color(0, 0, 0, newValue);
            } else
            {
                initiallyFaded = !initiallyFaded;
                countDown = 1;
                startedFade = false;
                onComplete?.Invoke();
            }
        }
    }

    public void StartFade(Action onComplete)
    {
        startedFade = true;
        this.onComplete = onComplete;
    }
}
