using UnityEngine;
using UnityEngine.VFX;

public class GunControlUI : MonoBehaviour
{
    [SerializeField] GameObject player, gun, muzzle;
    private Vector3 mouseWorldPosition, direction;
    private float angle = 0;
    [SerializeField] AudioSource audioSource;
    [SerializeField] VisualEffect effect;

    void Update()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        direction = (mouseWorldPosition - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.SetPositionAndRotation(
            Vector3.Lerp(transform.position, player.transform.position, 0.4f), 
            Quaternion.Euler(new Vector3(0, 0, angle)));
    }

    public void Fire()
    {
        audioSource.Play();
        effect.Play();
        Camera.main.gameObject.GetComponent<CameraMovementWelcome>().ScreenShake(1.2f, 3);
        //recoil on gun
        transform.position += -direction.normalized * 0.5f;
    }
}
