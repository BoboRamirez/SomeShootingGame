using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] AudioSource a;
    // Start is called before the first frame update
    public void Boom()
    {
        a.Play();
        Camera.main.gameObject.GetComponent<CameraMovement>().ScreenShake(0.8f, 3);
        IEnumerator v = Vanish();
        StartCoroutine(v);
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(5.2f);
        BulletPoolManager.Instance.ReturnExplosion(gameObject);
    }
}
