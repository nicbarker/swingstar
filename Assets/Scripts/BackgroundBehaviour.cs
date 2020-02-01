using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    public GameObject player;
    public float offset;
    private float localOffset;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;


        if (player.transform.position.x - (player.transform.position.x / 4) - spriteRenderer.bounds.size.x > offset + localOffset)
        {
            localOffset += spriteRenderer.bounds.size.x * 2;
        }
        transform.position = new Vector3(Mathf.Max((player.transform.position.x / 4) + offset + localOffset, 1.76f), transform.position.y, transform.position.z);
    }
}
