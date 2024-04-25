using UnityEngine;
using UnityEngine.VFX;

public class GunControl : MonoBehaviour
{
    [SerializeField] GameObject player, gun, muzzle;
    private Vector3 mouseWorldPosition, direction;
    private float angle = 0, split;
    [SerializeField] AudioSource audioSource;
    [SerializeField] VisualEffect effect;
    [SerializeField] float recoilAngle;
    int fireCount;
    void Start()
    {
        fireCount = 0;
    }

    void Update()
    {
        if (PlayerMove.Instance.IsDead)
            Destroy(gameObject);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        direction = (mouseWorldPosition - transform.position).normalized;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, player.transform.position, 0.4f), Quaternion.Euler(new Vector3(0, 0, angle)));
    }

    public void Fire()
    {
        GameObject[] newBullets = { BulletPoolManager.Instance.GetBullet(),
            BulletPoolManager.Instance.GetBullet(), BulletPoolManager.Instance.GetBullet() };
        GameObject newEffect = BulletPoolManager.Instance.GetBulletEffect();
        GameObject newShell = BulletPoolManager.Instance.GetShell();
        split = -1;
        foreach (var newBullet in newBullets)
        {
            //fire the bullet
            newBullet.GetComponent<Bullet>().Strike(muzzle.transform.position, 
                Quaternion.AngleAxis(UnityEngine.Random.Range(-recoilAngle/2, recoilAngle/2), Vector3.forward)
                * Quaternion.AngleAxis(recoilAngle * split, Vector3.forward)
                * direction,
                newEffect.GetComponent<BulletHitWallEffect>());
            split++;
        }
        newShell.GetComponent<Shell>().Eject(gun.transform.position, direction);
        audioSource.Play();
        effect.Play();
        Camera.main.gameObject.GetComponent<CameraMovement>().ScreenShake(-direction, 0.5f);
        fireCount++;
        //recoil on player
        player.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * 5, ForceMode2D.Impulse);
        //recoil on gun
        transform.position += -direction.normalized * 0.5f;
    }
}
