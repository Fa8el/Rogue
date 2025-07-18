using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject panelGameOver;

    void Start()
    {
        panelGameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ActivarGameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}



