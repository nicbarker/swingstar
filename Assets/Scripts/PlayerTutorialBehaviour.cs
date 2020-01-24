using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialBehaviour : MonoBehaviour
{
    private List<float> timers = new List<float>() { 4, 5, 6 };
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timers.Count > 0 && currentTime > timers[0])
        {
            timers.RemoveAt(0);
        }
        currentTime += Time.deltaTime;
    }
}
