using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterBoxControl : MonoBehaviour
{
    public static LetterBoxControl Instance;
    [SerializeField] TextMeshProUGUI m_TextMeshPro;
    [SerializeField] GameObject topGo, bottomGo, buttonGroup;
    RectTransform t, b, bg;
    [SerializeField] float speed;
    public string deathComment;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        m_TextMeshPro.text = "Heyheyhey!";
        m_TextMeshPro.color = new Color(1,1,1,0);
        t = topGo.GetComponent<RectTransform>();
        b = bottomGo.GetComponent<RectTransform>();
        bg = buttonGroup.GetComponent<RectTransform>();
        t.anchoredPosition = new Vector2(0, t.rect.height/2f);
        b.anchoredPosition = new Vector2(0, -b.rect.height/2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerMove.Instance.IsDead) return;
        m_TextMeshPro.text = deathComment;
        m_TextMeshPro.color = Vector4.MoveTowards(
            m_TextMeshPro.color, new Color(1, 1, 1, 1), Time.deltaTime * 1);
        t.anchoredPosition =
            Vector2.MoveTowards(t.anchoredPosition, new Vector2(0, -t.rect.height / 2f), Time.deltaTime * speed);
        b.anchoredPosition =
            Vector2.MoveTowards(b.anchoredPosition, new Vector2(0, b.rect.height / 2f), Time.deltaTime * speed);
        bg.anchoredPosition =
            Vector2.MoveTowards(bg.anchoredPosition, new Vector2(0, 0), Time.deltaTime * speed);

    }

    public void OnClickReplay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void OnClickQuit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
