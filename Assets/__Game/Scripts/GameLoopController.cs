using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLoopController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public AudioSource failAudioSource;
    public AudioSource winAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource noiseAudioSource;

    private DeadScreenController screenController;

    private void Start()
    {
        screenController = gameOverScreen.GetComponentInChildren<DeadScreenController>();
        screenController.SetContuinueCallback(ContinueAfterWon);
        Time.timeScale = 1f;
    }

    public void OnGameOver(GameOverEP param)
    {
        if (param.success)
        {
            winAudioSource.Play();
        } else
        {
            failAudioSource.Play();
        }

        musicAudioSource.volume = 0.3f;
        noiseAudioSource.volume = 0.4f;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;

        screenController.SetDeadScreenData(param.success, param.title, param.description);
    }

    public void ContinueAfterWon()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
