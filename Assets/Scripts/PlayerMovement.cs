using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 3f;
    private bool canMove = true;
    private string horizontalAxis = "Horizontal";

    private SpriteRenderer sr;
    private Animator playerAnim;

    private string runAnimatorName = "Run";

    [SerializeField]
    private float min_x = 0f, max_X = 0f;

    private string Cat_Tag = "Cat";

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        PlayerBounds();
    }

    void Movement()
    {
        if (!canMove)
            return;

        float h = Input.GetAxisRaw(horizontalAxis); 

        Vector2 tempPos = transform.position;

        if (h > 0) {
            tempPos.x += movementSpeed * Time.deltaTime;
            sr.flipX = false;

            playerAnim.Play(runAnimatorName);
        }
        else if (h < 0) {
            tempPos.x -= movementSpeed * Time.deltaTime;
            sr.flipX = true;

            playerAnim.Play(runAnimatorName);
        }
        if (h == 0)
        {
            playerAnim.Play("Idle");
        }

        transform.position = tempPos;
    }

    void PlayerBounds()
    {
        Vector2 tempPos = transform.position;

        if (tempPos.x > max_X)
        {
            tempPos.x = max_X;
        }
        else if (tempPos.x < min_x)
        {
            tempPos.x = min_x;
        }

        transform.position = tempPos;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;

        SceneManager.LoadScene("Gameplay");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Cat_Tag))
        {
            Time.timeScale = 0f;
            canMove = false;

            StartCoroutine(RestartGame());
        }
    }
}
