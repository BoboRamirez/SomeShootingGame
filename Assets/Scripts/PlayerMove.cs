using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;
    [SerializeField] private float jumpForce, speedLimit, horizontalSpeed, fireRate, friction;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded = false, isDead;
    [SerializeField] private GunControl gun;
    [SerializeField] private int horizontalInput;
    [SerializeField] private VisualEffect effect;
    [SerializeField] private AudioSource wasted;
    private float fireCountDown = 0, x;
    public float FireDelay
    {
        get
        {
            return 1.0f / fireRate;
        }
    }
    public bool IsDead
    {
        get { return isDead; }
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        fireCountDown = FireDelay;
        horizontalInput = 0;
        isDead = false;
    }
    private void FixedUpdate()
    {
        if (isDead)
            return;
        if ( horizontalInput != 0)
        {
            rb.velocity = new Vector2 (horizontalInput * horizontalSpeed, rb.velocity.y);
        }
        else
        {
            x = Mathf.MoveTowards(rb.velocity.x, 0, friction);
            rb.velocity = new Vector2(x, rb.velocity.y);
        }
        if (rb.velocity.magnitude >= speedLimit)
        {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        if (contact.normal.y > 0.8 && isGrounded == false)
        {
            isGrounded = true;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            Die($"{EnemyPoolManager.Instance.KillCount} kills. Not Bad...");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0.2f, 0.2f);
            return;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            isGrounded = false;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (fireCountDown >=  FireDelay)
            {
                gun.Fire();
                fireCountDown -= FireDelay;
            }
            else
            {
                fireCountDown += Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            horizontalInput = 0;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;
        }
        else
        {
            horizontalInput = 0;
        }
    }
    public void Die(string dc = "Fire or fade, and you are faded...")
    {
        isDead = true;
        Time.timeScale = 0.6f;
        gameObject.layer = LayerMask.NameToLayer("Remain");
        rb.AddForce(rb.velocity * 50, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
        rb.freezeRotation = false;
        rb.AddTorque(20);
        wasted.Play();
        Camera.main.GetComponent<CameraMovement>().Zoom(30f);
        LetterBoxControl.Instance.deathComment = dc;
    }


/*    private void OnGUI()
    {
        // Display the velocity on the screen
        GUI.Label(new Rect(10, 10, 300, 20), "Velocity: " + rb.velocity);
    }*/
}

