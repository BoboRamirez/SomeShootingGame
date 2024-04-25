using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;
    public GameObject bulletPrefab, bulletEffetPrefab, shellPrefab, explosionPrefab;
    public Queue<GameObject> bulletPool = new Queue<GameObject>(),
        bulletEffectPool = new Queue<GameObject>(), 
        shellPool = new Queue<GameObject>(),
        explosionPool = new Queue<GameObject>();
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        InitializeBullets(100);
    }

    void InitializeBullets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            GameObject bulletEffect = Instantiate(bulletEffetPrefab);
            GameObject shell = Instantiate(shellPrefab);
            GameObject explosion = Instantiate(explosionPrefab);
            bullet.SetActive(false);
            bulletEffect.SetActive(false);
            shell.SetActive(false);
            explosion.SetActive(false);
            bulletPool.Enqueue(bullet);
            bulletEffectPool.Enqueue(bulletEffect);
            shellPool.Enqueue(shell);
            explosionPool.Enqueue(explosion);
        }

    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // Optionally expand the pool if empty
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(true);
            return newBullet;
        }
    }

    public GameObject GetBulletEffect()
    {
        if (bulletEffectPool.Count > 0)
        {
            GameObject bulletEffect = bulletEffectPool.Dequeue();
            bulletEffect.SetActive(true);
            return bulletEffect;
        }
        else
        {
            return null;
        }
    }
    public GameObject GetShell()
    {
        if (shellPool.Count > 0)
        {
            GameObject shell = shellPool.Dequeue();
            shell.SetActive(true);
            return shell;
        }
        else
        {
            GameObject shell = Instantiate(shellPrefab);
            shell.SetActive(true);
            return shell;
        }
    }
    public GameObject GetExplosion()
    {
        if (explosionPool.Count > 0)
        {
            GameObject ex = explosionPool.Dequeue();
            ex.SetActive(true);
            return ex;
        }
        else
        {
            GameObject ex = Instantiate(explosionPrefab);
            ex.SetActive(true);
            return ex;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
    public void ReturnBulletEffect(GameObject bulletEffect)
    {
        bulletEffect.SetActive(false);
        bulletEffectPool.Enqueue(bulletEffect);
    }
    public void ReturnShell(GameObject shell)
    {
        shell.SetActive(false);
        shellPool.Enqueue(shell);
    }
    public void ReturnExplosion(GameObject explosion)
    {
        explosion.SetActive(false);
        explosionPool.Enqueue(explosion);
    }
/*    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "bullet size: " + bulletPool.Count);
    }*/
}
