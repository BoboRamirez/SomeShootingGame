using System.Collections;
using UnityEngine;
public class CameraMovementWelcome : MonoBehaviour
{
    Vector3 targetPos = new Vector3(0, 0, -10), curPos;
    private void LateUpdate()
    {
        curPos = transform.position;
        transform.position = Vector3.Lerp(curPos, targetPos, Time.deltaTime * 50);
    }
    public void ScreenShake(float amp, int count)
    {
        IEnumerator s = Shake(amp, count);
        StartCoroutine(s);
    }
    IEnumerator Shake (float amp, int count)
    {
        while (count > 0)
        {
            float angle = Random.Range(0, 360);
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            transform.position += direction.normalized * amp;
            count--;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
