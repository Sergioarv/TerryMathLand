using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    public float velocidadMAX = 3.6f;
    public float fuerzaSalto = 4f;

    Rigidbody2D rbPlayer;
    SpriteRenderer spritePlayer;
    Animator animatorPlayer;

    bool mirandoDerecha = true;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        float movX = Input.GetAxis("Horizontal");

        if (movX != 0)
        {
            rbPlayer.velocity = new Vector2(movX * velocidadMAX + Time.deltaTime, rbPlayer.velocity.y);
            animatorPlayer.SetBool("Run", true);
        }
        else
        {
            rbPlayer.velocity = new Vector2(0 * velocidadMAX + Time.deltaTime, rbPlayer.velocity.y);
            animatorPlayer.SetBool("Run", false);
        }

        if (Input.GetKey("space") && VerificarSuelo.tocandoSuelo)
        {
            Debug.Log("Salto tocando suelo");
            rbPlayer.AddForce(new Vector2(0, fuerzaSalto));
            animatorPlayer.SetBool("Jump", true);
            animatorPlayer.SetBool("Run", false);
        }
        else if (VerificarSuelo.tocandoSuelo)
        {
            Debug.Log("Tocando suelo");
            animatorPlayer.SetBool("Jump", false);
        }
        else if (Input.GetKey("space") && !VerificarSuelo.tocandoSuelo)
        {
            Debug.Log("Salto sin suelo");
        }

        if (movX > 0 && !mirandoDerecha)
        {
            volteo();
        }
        else if (movX < 0 && mirandoDerecha)
        {
            volteo();
        }
    }

    void volteo()
    {
        mirandoDerecha = !mirandoDerecha;
        spritePlayer.flipX = mirandoDerecha;
    }


}
