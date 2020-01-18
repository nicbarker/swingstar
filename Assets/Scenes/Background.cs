using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject player;
    public float offset;
    private float localOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (player.transform.position.x - (player.transform.position.x / 4) - sprite.bounds.size.x > offset + localOffset)
        {
            localOffset += sprite.bounds.size.x * 2;
        }
        transform.position = new Vector3(Mathf.Max((player.transform.position.x / 4) + offset + localOffset, 1.76f), transform.position.y, transform.position.z);
    }
}
