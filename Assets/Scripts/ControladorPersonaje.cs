using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    public float velocidadMAX = 3.6f;
    public float fuerzaSalto = 4f;

    Rigidbody2D rbPlayer;
    SpriteRenderer spritePlayer;
    Animator animatorPlayer;

    bool mirandoDerecha;

    float movX;

    public GameObject mano;
    public Vector2 posMano;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();

        mirandoDerecha = spritePlayer.flipX;
        posMano = mano.transform.position;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        movX = Input.GetAxisRaw("Horizontal");

        if (movX != 0)
        {
            rbPlayer.velocity = new Vector2(movX * velocidadMAX + Time.deltaTime, rbPlayer.velocity.y);
            animatorPlayer.SetBool("Run", true);
            animatorPlayer.SetFloat("UltimoX", movX);
        }
        else
        {
            rbPlayer.velocity = new Vector2(0 * velocidadMAX + Time.deltaTime, rbPlayer.velocity.y);
            animatorPlayer.SetBool("Run", false);
        }

        if (Input.GetKey("space") && VerificarSuelo.tocandoSuelo)
        {
            rbPlayer.AddForce(new Vector2(0, fuerzaSalto));
            animatorPlayer.SetBool("Jump", true);
            animatorPlayer.SetBool("Run", false);
        }
        else if (VerificarSuelo.tocandoSuelo)
        {
            animatorPlayer.SetBool("Jump", false);
        }
        else if (Input.GetKey("space") && !VerificarSuelo.tocandoSuelo)
        {
            Debug.Log("Salto sin suelo");
        }

        volteo();
        posicionMano();
    }

    void volteo()
    {
        if (movX > 0.1f && !mirandoDerecha)
        {
            spritePlayer.flipX = true;
            mirandoDerecha = true;
        }

        if (movX < 0.1f && mirandoDerecha)
        {
            spritePlayer.flipX = false;
            mirandoDerecha = false;
        }

        if (movX == 0)
        {
            if (animatorPlayer.GetFloat("UltimoX") == 1 && !mirandoDerecha)
            {
                spritePlayer.flipX = true;
                mirandoDerecha = true;
            }

            if (animatorPlayer.GetFloat("UltimoX") == -1 && mirandoDerecha)
            {
                spritePlayer.flipX = false;
                mirandoDerecha = false;
            }
        }
    }

    void posicionMano()
    {

        int hijosMano = mano.transform.childCount;

        if (movX != 0)
        {
            float vX = movX * 0.3f;
            mano.transform.localPosition = new Vector2(vX, 0);
            mano.transform.GetChild(0).localPosition = new Vector2(vX, 0.4f);
            if (hijosMano > 1) mano.transform.GetChild(1).localPosition = new Vector2(vX, 0.4f);
        }
    }

}
