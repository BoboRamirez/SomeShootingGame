using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed, direction = 1, knockBackForce;
    [SerializeField] private int Hp;
    [SerializeField] private MeshRenderer _render;
    [SerializeField] private bool isGrounded = false;
    Vector2 Vel
    {
        get
        {
            return new Vector2(speed * direction, rb.velocity.y);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vel;
        Hp = 3;
    }

    private void Update()
    {
        if (rb.velocity.magnitude < 0.001)
        {
            direction *= -1;
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
        else if (rb.velocity.magnitude < speed && isGrounded)
        {
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        if (collision.collider.CompareTag("Projectile"))
        {
            isGrounded = false;
            TakeDamage(1, (-contact.normal + Vector2.up).normalized);
        }
        if (contact.normal.y > 0.8 && isGrounded == false)
        {
            isGrounded = true;
        }
    }
/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Projectile"))
        {
            Debug.Log("hit by bullet");
            TakeDamage(1);
        }
    }*/

    public void TakeDamage(int damage, Vector3 direction)
    {
        if (!gameObject.activeSelf)
            return;
        Hp -= damage;
        rb.AddForce(direction.normalized * knockBackForce, ForceMode2D.Impulse);
        IEnumerator c = Flash();
        StopAllCoroutines();
        StartCoroutine(c);
        if (Hp <= 0)
        {
            GameManager.Instance.SlowMotionOnDeath();
            GameObject body = EnemyPoolManager.Instance.GetEnemyBody();
            if (body != null)
            {
                EnemyBody b = body.GetComponent<EnemyBody>();
                b.InitializeBody(direction.normalized, 
                    knockBackForce, transform.position);
            }
            EnemyPoolManager.Instance.ReturnEnemy(gameObject);
        }
    }
    private IEnumerator Flash()
    {
        MaterialPropertyBlock props = new();
        props.SetColor("_color", Color.white);
        _render.SetPropertyBlock(props);
        yield return new WaitForSeconds(0.1f);
        props.SetColor("_color", Color.red);
        _render.SetPropertyBlock(props);
    }

    public void Rise()
    {
        rb.velocity = Vel;
        Hp = 3;
        MaterialPropertyBlock props = new();
        props.SetColor("_color", Color.red);
        _render.SetPropertyBlock(props);
    }
    
}
