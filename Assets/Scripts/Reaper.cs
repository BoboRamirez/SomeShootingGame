using System.Collections;
using UnityEngine;
using static UnityEngine.Mathf;

public class Reaper : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    private int enemyCount;
    private Color c, flickColor;
    private float fade, targetAlpha;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        fade = 64;
        flag = true;
        IEnumerator sc = SwitchColor();
        StartCoroutine(sc);
    }

    // Update is called once per frame
    void Update()
    {
        if ( PlayerMove.Instance.IsDead)
        {
            sr.color = Vector4.MoveTowards(sr.color, new Color(0.5f, 0.3f, 0.2f, 1), 0.2f * Time.deltaTime);
            StopAllCoroutines();
            return;
        }
        c = sr.color;
        enemyCount = EnemyPoolManager.Instance.EnemyCount;
        float rate = (Pow(fade, Clamp((enemyCount / 80f - 0.5f) * 2, 0f, 1f)) - 1) / (fade - 1);
        if (rate >= 1f)
        {
            sr.color = flickColor;
        }
        else
        {
            targetAlpha = Lerp(0f, 1f, rate);
            c.a = MoveTowards(c.a, targetAlpha, 0.1f * Time.deltaTime);
            sr.color = c;
        }
    }
    IEnumerator SwitchColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (flag)
            {
                flickColor = new Color(1, 1, 1, 1);
            }
            else
            {
                flickColor = new Color(0.5f, 0.3f, 0.2f, 1);
            }
            flag = !flag;
        }
    }
}
