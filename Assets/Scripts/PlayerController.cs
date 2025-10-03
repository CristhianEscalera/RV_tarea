using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float jumpForce = 8f;

    [Header("Raycast para detectar suelo")]
    public float groundRayDistance = 0.2f;
    public LayerMask groundLayer;
    public Transform rayOriginLeft;
    public Transform rayOriginCenter;
    public Transform rayOriginRight;

    [Header("Vida y Monedas")]
    public int life = 3;
    public int Coins = 0;

    Rigidbody2D rb;
    Animator anim;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        UpdateLifeUI();
        UpdateCoinsUI();
    }

    void Update()
    {
        // ===> Detectar si está tocando el suelo con Raycasts
        isGrounded = IsTouchingGround();

        // ===> Movimiento horizontal
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;
        else if (Input.GetKey(KeyCode.A)) moveInput = -1f;

        rb.velocity = new Vector2(moveInput * velocidad, rb.velocity.y);

        if (moveInput != 0)
        {
            transform.rotation = Quaternion.Euler(0, moveInput < 0 ? 180 : 0, 0);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        // ===> Salto
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("Jump", true);
        }

        // ===> Al tocar suelo, apagar animación de salto
        if (isGrounded && anim.GetBool("Jump"))
        {
            anim.SetBool("Jump", false);
        }
    }

    bool IsTouchingGround()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(rayOriginLeft.position, Vector2.down, groundRayDistance, groundLayer);
        RaycastHit2D hitCenter = Physics2D.Raycast(rayOriginCenter.position, Vector2.down, groundRayDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rayOriginRight.position, Vector2.down, groundRayDistance, groundLayer);

        return hitLeft.collider != null || hitCenter.collider != null || hitRight.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            Coins++;
            Destroy(collision.gameObject);
            UpdateCoinsUI();
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Enemy_Anim") || collision.CompareTag("Deadzone"))
        {
            life--;
            if (collision.CompareTag("Enemy_Anim") || collision.CompareTag("Deadzone"))
            {
                life = 0;
            }

            UpdateLifeUI();

            if (life <= 0)
            {
                ShowDeathMessage();
                Destroy(gameObject);
            }
        }
    }

    void UpdateLifeUI()
    {
        Text lifeText = GameObject.FindWithTag("Life")?.GetComponent<Text>();
        if (lifeText != null)
            lifeText.text = life.ToString();
    }

    void UpdateCoinsUI()
    {
        Text coinText = GameObject.FindWithTag("CountCoin")?.GetComponent<Text>();
        if (coinText != null)
            coinText.text = Coins.ToString();
    }

    void ShowDeathMessage()
    {
        Text deathText = GameObject.FindWithTag("DeathMenssage")?.GetComponent<Text>();
        if (deathText != null)
        {
            Color color = deathText.color;
            color.a = 1f;
            deathText.color = color;
        }
    }

    // Visualizar los raycasts en la escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (rayOriginLeft != null)
            Gizmos.DrawLine(rayOriginLeft.position, rayOriginLeft.position + Vector3.down * groundRayDistance);
        if (rayOriginCenter != null)
            Gizmos.DrawLine(rayOriginCenter.position, rayOriginCenter.position + Vector3.down * groundRayDistance);
        if (rayOriginRight != null)
            Gizmos.DrawLine(rayOriginRight.position, rayOriginRight.position + Vector3.down * groundRayDistance);
    }
}
