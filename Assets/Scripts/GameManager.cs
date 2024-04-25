using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int killCount = 0;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlowMotionOnDeath()
    {
        IEnumerator ds = DieSlowMotion();
        if (Time.timeScale == 1f)
        {
            StartCoroutine(ds);
        }
    }

    private IEnumerator DieSlowMotion()
    {
        Time.timeScale = 0.8f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;
    }
/*    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "timescale: " + Time.timeScale);
    }*/
}
