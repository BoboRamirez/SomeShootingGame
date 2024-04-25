using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float explosionRadius;
    BulletHitWallEffect effect;
    public static float Speed = 100f;
    private bool isFlying;
/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Blocks"))
        {
            ContactPoint2D[] contacts = new ContactPoint2D[20];
            collision.GetContacts(contacts);
            for (int i = 0; i < contacts.Length; i++)
            {
                Debug.Log(contacts[i].normal.normalized);
            }
            effect.SetVector3("bounceDirection", contacts[0].normal.normalized);
            effect.Play();
            BulletPoolManager.Instance.ReturnBullet(gameObject);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (isFlying && (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Blocks")))
        {
            isFlying = false;
            effect.Spark(transform.position, collision.GetContact(0).normal.normalized);
            Explode(transform.position);
            BulletPoolManager.Instance.ReturnBullet(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 100 ||  transform.position.y > 80
            || transform.position.x  < -20 || transform.position.y < -20)
        {
            BulletPoolManager.Instance.ReturnBullet(gameObject);
            BulletPoolManager.Instance.ReturnBulletEffect(effect.gameObject);
        }
    }
    public void Strike(Vector3 p, Vector3 d, BulletHitWallEffect bhe)
    {
        transform.position = p;
        rb.velocity = Speed * d.normalized;
        effect = bhe;
        isFlying = true;
    }

    public void Explode(Vector3 pos)
    {
        int rad = UnityEngine.Random.Range(0, 20);
        if (rad != 0)
            return;
        GameObject exGo = BulletPoolManager.Instance.GetExplosion();
        exGo.transform.position = pos;
        exGo.GetComponent<Explosion>().Boom();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (!collider.CompareTag("Enemy"))
                continue;
            GameObject e = collider.gameObject;
            e.GetComponent<Enemy>().TakeDamage(2, e.transform.position - pos);
        }
    }

}
