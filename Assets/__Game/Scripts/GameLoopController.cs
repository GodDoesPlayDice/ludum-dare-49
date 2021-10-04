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

    public void OnGameOver(GameOverEP gameOverEP)
    {
        if (gameOverEP.success)
        {
            winAudioSource.Play();
        } else
        {
            failAudioSource.Play();
        }

        musicAudioSource.volume = 0.3f;
        noiseAudioSource.volume = 0.4f;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
