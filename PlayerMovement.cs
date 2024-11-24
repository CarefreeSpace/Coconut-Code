using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed, jumpStrength;
    public int numberOfJumps;
    public string nextLevel;
    public int saveSlot;
    public Timer timer;
    public AudioSource jump, death, win;

    private Rigidbody2D rb;
    private PlayerControls controls;
    private SpriteRenderer sprite;
    private int currentNumberOfJumps;
    private float movement;
    private bool isGround;

    void Awake()
    {
        if (timer.timeTrial == true)
        {
            string e = timer.LoadTimeFromSlot(saveSlot);
        }

        rb = this.GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        sprite = this.GetComponent<SpriteRenderer>();
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<float>();
        controls.Player.Move.canceled += _ => movement = 0;
        controls.Player.Jump.started += _ => Jump();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        if (movement > 0)
        {
            sprite.flipX = false;
        }
        else if (movement < 0)
        {
            sprite.flipX = true;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement * movementSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (currentNumberOfJumps >= 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength);
            jump.Play();
            currentNumberOfJumps -= 1;
        }
    }

    void ResetJumps()
    {
        if (isGround)
        {
            currentNumberOfJumps = numberOfJumps;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            ResetJumps();
        }
        else if (other.gameObject.tag == "Death")
        {
            StartCoroutine(DelayReloadScene());
        }
        else if (other.gameObject.tag == "Win")
        {
            timer.SaveTimeToSlot(saveSlot);
            StartCoroutine(DelayLoadNextLevel());
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    IEnumerator DelayReloadScene()
    {
        death.Play();
        yield return new WaitForSeconds(1.8f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator DelayLoadNextLevel()
    {
        win.Play();
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(nextLevel);
    }
}