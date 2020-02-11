using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject chainPrefab;
    public List<AudioClip> shootSounds;
    private int currentSoundSource = 0;

    private bool tutorialVisible;
    private float tutorialTimer = 0.8f;
    private Vector2 tutorialVelocity;
    private float tutorialAngularVelocity;
    public bool gameStarted = false;

    private bool atInitialPosition = true;
    private bool mouseClicked;
    private Vector3 clickPoint;
    private GameObject currentRope;
    private Vector2 ropeEndPoint;
    private bool exploded;
    public float timer = 3;
    private float initialTimer = 3;
    public float maxRopeDistance = 30;
    public bool enableMouseEvents = true;
    private Vector3 initialPosition;

    private Rigidbody2D playerRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialTimer = timer;
        playerRigidBody = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        QualitySettings.maxQueuedFrames = 1;
    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPosition;
        playerRigidBody.velocity = new Vector2(0, 0);
        playerRigidBody.angularVelocity = 0;
        playerRigidBody.bodyType = RigidbodyType2D.Static;
        timer = initialTimer;
        ReleaseChain();
        atInitialPosition = true;
    }

    public void ShootTowardsPoint(Vector3 clickPoint)
    {
        this.mouseClicked = true;
        this.clickPoint = clickPoint;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        int hits = Physics2D.Raycast(transform.position, clickPoint - transform.position, new ContactFilter2D(), results, 100);
        if (hits > 1)
        {
            ropeEndPoint = results[1].point;
            Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
            // Instantiate at position (0, 0, 0) and zero rotation.
            currentRope = Instantiate(
                chainPrefab,
                new Vector3(
                    transform.position.x,
                    transform.position.y,
                    chainPrefab.transform.position.z
                ),
                Quaternion.Euler(
                    0, 0, 90 + Mathf.Atan2(playerPosition.y - clickPoint.y, playerPosition.x - clickPoint.x) * Mathf.Rad2Deg
                )
            );
            Physics2D.IgnoreCollision(currentRope.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
            float totalRopeDistance = Vector2.Distance(ropeEndPoint, transform.position) * (1 / currentRope.transform.localScale.x);
            currentRope.GetComponent<SpriteRenderer>().size = new Vector2(1, 6);
            HingeJoint2D hinge = currentRope.GetComponent<HingeJoint2D>();
            hinge.connectedBody = results[1].rigidbody;
            hinge.anchor = new Vector2(0, totalRopeDistance);
            FixedJoint2D fixedJoint = currentRope.GetComponent<FixedJoint2D>();
            fixedJoint.connectedBody = this.playerRigidBody;
            // Play rope shooting sound
            GetComponents<AudioSource>()[currentSoundSource].clip = shootSounds[0];
            GetComponents<AudioSource>()[currentSoundSource].Play();
            currentSoundSource = currentSoundSource == 0 ? 1 : 0;
        }
    }

    public void ReleaseChain()
    {
        this.clickPoint = new Vector2(0,0);
        this.mouseClicked = false;
        if (currentRope)
        {
            Destroy(currentRope);
            this.playerRigidBody.AddForce(this.playerRigidBody.velocity * 55);
            this.playerRigidBody.AddForce(new Vector2(0, 70));
        }
    }

    void LateUpdate()
    {
        if (!gameStarted)
        {
            this.mouseClicked = Input.GetMouseButton(0);
            return;
        } else if (atInitialPosition)
        {
            playerRigidBody.bodyType = RigidbodyType2D.Dynamic;
            playerRigidBody.AddForce(new Vector2(1500, 2000));
            atInitialPosition = false;
        }

        // Show tutorial on first play
        if (StaticData.showTutorial && enableMouseEvents)
        {
            if (tutorialTimer > 0)
            {
                tutorialTimer -= Time.deltaTime;
            } else
            {
                GameObject.Find("TutorialCanvas").GetComponent<Canvas>().enabled = true;
                StaticData.showTutorial = false;
                tutorialVisible = true;
                tutorialVelocity = playerRigidBody.velocity;
                tutorialAngularVelocity = playerRigidBody.angularVelocity;
                playerRigidBody.bodyType = RigidbodyType2D.Static;
            }
        }

        if (this.enableMouseEvents && !this.mouseClicked && Input.GetMouseButton(0))
        {
            // Disable the tutorial if the user has already tapped or clicked before it's shown
            StaticData.showTutorial = false;
            if (tutorialVisible)
            {
                playerRigidBody.bodyType = RigidbodyType2D.Dynamic;
                playerRigidBody.velocity = tutorialVelocity;
                playerRigidBody.angularVelocity = tutorialAngularVelocity;
                GameObject.Find("TutorialCanvas").GetComponent<Canvas>().enabled = false;
            }
            ShootTowardsPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        } else if (this.enableMouseEvents && this.mouseClicked && !Input.GetMouseButton(0) && !exploded)
        {
            ReleaseChain();
        }

        if (currentRope)
        {
            SpriteRenderer sprite = currentRope.GetComponent<SpriteRenderer>();
            float totalRopeDistance = Vector2.Distance(ropeEndPoint, transform.position) * (1 / currentRope.transform.localScale.x);
            if (ropeEndPoint != new Vector2(0, 0) && sprite.size.y < totalRopeDistance)
            {
                sprite.size = new Vector2(2.49f, Mathf.Min(sprite.size.y + 10, totalRopeDistance));
                currentRope.transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    currentRope.transform.position.z
                );
                GameObject chainHead = transform.GetChild(0).gameObject;
                chainHead.transform.position += (currentRope.transform.forward * 0.05f);
                currentRope.transform.rotation = Quaternion.Euler(
                    0, 0, 90 + Mathf.Atan2(transform.position.y - clickPoint.y, transform.position.x - clickPoint.x) * Mathf.Rad2Deg
                );
            }

            if (ropeEndPoint != new Vector2(0, 0) && sprite.size.y >= totalRopeDistance)
            {
                sprite.size = new Vector2(2.49f, sprite.size.y + 0.5f);
                currentRope.GetComponent<HingeJoint2D>().enabled = true;
                currentRope.GetComponent<FixedJoint2D>().enabled = true;
                ropeEndPoint = new Vector2(0, 0);
            } else if (ropeEndPoint != new Vector2(0, 0) && sprite.size.y >= maxRopeDistance)
            {
                ReleaseChain();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.exploded)
        {
            if (currentRope != null)
            {
                Destroy(currentRope);
            }

            var explodable = GetComponent<Explodable>();
            if (explodable) {
                explodable.explode();
            }
            this.exploded = true;
        }
    }
}
