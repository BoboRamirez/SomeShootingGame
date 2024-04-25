using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeUIControl : MonoBehaviour
{
    [SerializeField] GunControlUI gun;
    private bool isActive;

    private void Start()
    {
        Debug.Log("start");
        isActive = true;
    }

    public void OnClickPlay()
    {
        if (!isActive)
            return;
        isActive = false;
        Debug.Log("play!");
        gun.Fire();
        IEnumerator startplay = Play();
        StartCoroutine(startplay);
    }
    public void OnClickQuit()
    {
        if (!isActive)
            return;
        isActive = false;
        Debug.Log("quit!");
        Application.Quit();
    }
    private IEnumerator Play()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Stage");
    }
}
