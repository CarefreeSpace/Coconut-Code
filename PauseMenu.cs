using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public PlayerMovement player;
    public Rigidbody2D playerBody;
    public GameObject pauseMenu;
    public AudioSource mainMusic, pauseMusic, chainFalling, chainDropping;
    public Timer timer;

    private PlayerControls controls;
    private const string UnloadAnimation = "Falling";
    private const string LoadAnimation = "Dropping";

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Paused.started += _ => PasueGame();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PasueGame()
    {
        pauseMenu.SetActive(true);
        PlayAnimation(pauseMenu, LoadAnimation);
        chainDropping.Play();
        pauseMusic.Play();
        mainMusic.Stop();
        player.enabled = false;
        playerBody.gravityScale = 0;
        playerBody.linearVelocity = new Vector2(0,0);
        timer.timerOn = false;
    }

    public void ResumeGame()
    {
        PlayAnimation(pauseMenu, UnloadAnimation);
        chainFalling.Play();
        pauseMusic.Stop();
        mainMusic.Play();
        player.enabled = true;
        playerBody.gravityScale = 1;
        timer.timerOn = true;
        pauseMenu.SetActive(false);
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