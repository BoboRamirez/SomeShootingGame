using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] MeshRenderer mr;
    public void InitializeBody(Vector3 direction, float knockBackForce, Vector3 pos)
    {
        transform.SetPositionAndRotation(pos, Quaternion.identity);
        rb.AddForce((-direction + new Vector3(0, 0.3f, 0)).normalized * knockBackForce*5, ForceMode2D.Impulse);
        rb.AddTorque(knockBackForce * 4, ForceMode2D.Impulse);
        MaterialPropertyBlock props = new();
        props.SetColor("_color", new Color(0, 0.5f, 0.8f, 0.5f));
        mr.SetPropertyBlock(props);
        IEnumerator bc = BodyCollection();
        StartCoroutine(bc);
    }
    IEnumerator BodyCollection()
    {
        yield return new WaitForSeconds(8 * 60);
        EnemyPoolManager.Instance.ReturnBody(gameObject);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 2 * Time.fixedDeltaTime);
    }
}
