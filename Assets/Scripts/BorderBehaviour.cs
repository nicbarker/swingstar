using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderBehaviour : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public GameObject[] obstacles = new GameObject[3];
    private GameObject obstacleOne;
    private GameObject obstacleTwo;
    private GameObject obstacleThree;
    private float localOffset;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateObstacles();
    }

    void InstantiateObstacles()
    {
        float obstacleDistance = 48.334f / 3;
        // Pick three random objects to add
        System.Random random = new System.Random();
        GameObject one = obstacles[random.Next(0, 3)];
        GameObject two = obstacles[random.Next(0, 3)];
        GameObject three = obstacles[random.Next(0, 3)];
        obstacleOne = Instantiate(one, new Vector3(offset + localOffset, one.transform.position.y, one.transform.position.z), Quaternion.identity);
        obstacleTwo = Instantiate(two, new Vector3(offset + localOffset + obstacleDistance, two.transform.position.y, two.transform.position.z), Quaternion.identity);
        obstacleThree = Instantiate(three, new Vector3(offset + localOffset + (obstacleDistance * 2), three.transform.position.y, three.transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.transform.position.x - 48.334f > offset + localOffset)
        {
            localOffset += 48.334f * 2;
            Destroy(obstacleOne);
            Destroy(obstacleTwo);
            Destroy(obstacleThree);
            InstantiateObstacles();
        }
        transform.position = new Vector3(offset + localOffset, transform.position.y, transform.position.z);
    }
}
