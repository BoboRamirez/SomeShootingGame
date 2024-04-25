using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private Vector3 dir;
    public void Eject(Vector3 pos, Vector3 gunDir)
    {
        transform.SetPositionAndRotation(pos, Quaternion.identity);
        dir = new Vector3(gunDir.y, -gunDir.x, gunDir.z);
        if (dir.y < 0)
            dir = -dir;
        dir -= gunDir * 0.4f;
        rb.AddForce(dir.normalized * 50);
        rb.AddTorque(2);
        IEnumerator sc = ShellCollection();
        StartCoroutine(sc);
    }
    IEnumerator ShellCollection()
    {
        yield return new WaitForSeconds(8 * 60);
        BulletPoolManager.Instance.ReturnShell(gameObject);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 2 * Time.fixedDeltaTime);
    }
}
