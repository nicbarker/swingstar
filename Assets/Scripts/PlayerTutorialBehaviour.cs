using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialBehaviour : MonoBehaviour
{
    private bool swinging;
    private List<float> baseTimers = new List<float>() { 1.8f, 2.2f, 2.7f, 3.05f, 3.6f, 4.1f, 4.8f };
    private List<float> timers;
    private float resetTimer = 6;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        timers = new List<float>(baseTimers.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        if (timers.Count > 0 && currentTime > timers[0])
        {
            timers.RemoveAt(0);
            var player = GameObject.Find("Player");
            if (!swinging)
            {
                player.GetComponent<PlayerBehaviour>().ShootTowardsPoint(player.transform.position + new Vector3(1.2f, 1.8f, 0));
                swinging = true;
            } else
            {
                player.GetComponent<PlayerBehaviour>().ReleaseChain();
                swinging = false;
            }
        }

        if (currentTime > resetTimer)
        {
            GameObject.Find("Player").GetComponent<PlayerBehaviour>().ResetPlayerPosition();
            timers = new List<float>(baseTimers.ToArray());
            currentTime = 0;
            swinging = false;
        }
        currentTime += Time.deltaTime;
    }
}
