using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    private bool isSeen;
    public bool IsSeen
    { get { return isSeen; } }
    private Vector3 viewportPos;
    // Start is called before the first frame update
    private void Update()
    {
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x >=0 && viewportPos.x <= 1 
            && viewportPos.y >= 0 && viewportPos.y <= 1)
        {
            isSeen = true;
        }
        else
        {
            isSeen = false;
        }
    }
/*    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "Mouse Position: " + viewportPos);
    }*/
}
