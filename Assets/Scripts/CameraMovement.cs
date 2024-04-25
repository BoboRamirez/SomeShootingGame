using System.Collections;
using UnityEngine;
using static UnityEngine.Mathf;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed, maxRange, minSpeed, depthOffset, fov;
    float distance, speed;
    Vector2 targetPos, curPos, mouseOffset;
    Vector3 mouseWorldPosition;
    private void LateUpdate()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        mouseOffset = (Vector2)(mouseWorldPosition - target.position) * 0.2f;
        targetPos = new Vector2(target.position.x, target.position.y) + mouseOffset;
        curPos = new Vector2(transform.position.x, transform.position.y);

        distance = (curPos - targetPos).magnitude;
        distance = distance > 0.0 ? distance : -distance;
        speed = Mathf.Lerp(minSpeed, maxSpeed, distance / maxRange);
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(targetPos.x, targetPos.y, depthOffset), Time.deltaTime * speed);
/*        Debug.Log("pos: " + curPos + transform.position);*/
        Camera.main.fieldOfView = MoveTowards(Camera.main.fieldOfView, fov, 50 * Time.deltaTime);
    }
    public void ScreenShake(Vector3 direction, float amp)
    {
        transform.position += direction.normalized * amp;
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
    public void Zoom (float newFov)
    {
        fov = newFov;
    }
/*    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + Input.mousePosition);
        GUILayout.Label("Mouse position: " + mouseWorldPosition);
        GUILayout.Label("cam position before: " + curPos);
        GUILayout.Label("cam position after: " + Camera.main.transform.position);
        GUILayout.EndArea();
    }*/
}
