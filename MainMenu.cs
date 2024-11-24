using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, levelSelect, settings, videoSettings, audioSettings;
    public GameObject mainMenuButton, levelSelectButton, settingsButton, videoSettingsButton, audioSettingsButton;
    public AudioSource mainMusic, selectMusic, chainFalling, chainDropping;
    public AudioMixer master;
    public EventSystem eventSystem;

    private const string UnloadAnimation = "Falling";
    private const string LoadAnimation = "Dropping";
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SetFullScreen(bool value)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetMusicVolume(float value)
    {
        master.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        master.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void UnloadMainMenu(GameObject otherMenu)
    {
        PlayAnimation(mainMenu, UnloadAnimation);
        chainFalling.Play();
        otherMenu.SetActive(true);
        PlayAnimation(otherMenu, LoadAnimation);
        chainDropping.Play();

        if (otherMenu == levelSelect)
        {
            eventSystem.SetSelectedGameObject(levelSelectButton);
            selectMusic.Play();
            mainMusic.Stop();
        }
        else if (otherMenu == settings)
        {
            eventSystem.SetSelectedGameObject(settingsButton);
        }
    }

    public void UnloadLevelSelect(GameObject otherMenu)
    {
        PlayAnimation(levelSelect, UnloadAnimation);
        chainFalling.Play();
        otherMenu.SetActive(true);
        PlayAnimation(otherMenu, LoadAnimation);
        chainDropping.Play();
        selectMusic.Stop();
        mainMusic.Play();

        if (otherMenu == mainMenu)
        {
            eventSystem.SetSelectedGameObject(mainMenuButton);
        }
    }

    public void UnloadSettings(GameObject otherMenu)
    {
        PlayAnimation(settings, UnloadAnimation);
        chainFalling.Play();
        otherMenu.SetActive(true);
        PlayAnimation(otherMenu, LoadAnimation);
        chainDropping.Play();

        if (otherMenu == mainMenu)
        {
            eventSystem.SetSelectedGameObject(mainMenuButton);
        }
        else if (otherMenu == videoSettings)
        {
            eventSystem.SetSelectedGameObject(videoSettingsButton);
        }
        else if (otherMenu == audioSettings)
        {
            eventSystem.SetSelectedGameObject(audioSettingsButton);
        }
    }

    public void UnloadVideoSettings(GameObject otherMenu)
    {
        PlayAnimation(videoSettings, UnloadAnimation);
        chainFalling.Play();
        otherMenu.SetActive(true);
        PlayAnimation(otherMenu, LoadAnimation);
        chainDropping.Play();

        if (otherMenu == settings)
        {
            eventSystem.SetSelectedGameObject(settingsButton);
        }
    }

    public void UnloadAudioSettings(GameObject otherMenu)
    {
        PlayAnimation(audioSettings, UnloadAnimation);
        chainFalling.Play();
        otherMenu.SetActive(true);
        PlayAnimation(otherMenu, LoadAnimation);
        chainDropping.Play();

        if (otherMenu == settings)
        {
            eventSystem.SetSelectedGameObject(settingsButton);
        }
    }

    private void PlayAnimation(GameObject target, string animationName)
    {
        if (target != null)
        {
            Animator animator = target.GetComponent<Animator>();
            
            if (animator != null)
            {
                animator.Play(animationName);
            }
            else
            {
                Debug.LogWarning($"No Animator found on {target.name}.");
            }
        }
    }
}