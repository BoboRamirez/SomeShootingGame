using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BulletHitWallEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] VisualEffect effect;
    [SerializeField] AudioSource aus;
    public void Spark(Vector3 pos,  Vector2 direction)
    {
        //aus.Play();
        gameObject.transform.position = pos;
        effect.SetVector2("bounceDirection", direction);
        /*Debug.Log("recycle effect");*/
        effect.Play();
        IEnumerator c = CountDown();
        StopAllCoroutines();
        StartCoroutine(c);
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3f);
        BulletPoolManager.Instance.ReturnBulletEffect(gameObject);
    }
}
