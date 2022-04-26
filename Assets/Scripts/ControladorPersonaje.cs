using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    public float velocidadMAX = 3.6f;
    public float fuerzaSalto = 4f;
    Rigidbody2D rbPlayer;
    SpriteRenderer spritePlayer;
    Animator animatorPlayer;

    bool mirandoDerecha = true;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if(direccion.x != 0)
        {
            animatorPlayer.SetBool("Run", true);
        }
        else
        {
            animatorPlayer.SetBool("Run", false);
        }
        */
        /*
        if (tocandoElSuelo && Input.GetAxis("Jump") > 0) 
        {
            tocandoElSuelo = false;
            rbPlayer.AddForce(new UnityEngine.Vector2(0, fuerzaSalto));
        }
        */
    }

    private void FixedUpdate()
    {
        Debug.Log(VerificarSuelo.tocandoSuelo);

        float movX = Input.GetAxis("Horizontal");

        if(movX != 0)
        {
            rbPlayer.velocity = new Vector2(movX * velocidadMAX + Time.fixedDeltaTime, rbPlayer.velocity.y);
            animatorPlayer.SetBool("Run", true);
        }
        else
        {
            rbPlayer.velocity = new Vector2(0 * velocidadMAX + Time.fixedDeltaTime, rbPlayer.velocity.y);
            animatorPlayer.SetBool("Run", false);
        }

        if(Input.GetKey("space") && VerificarSuelo.tocandoSuelo)
        {
            rbPlayer.AddForce(new Vector2(0, fuerzaSalto) );
            animatorPlayer.SetBool("Jump", true);
            animatorPlayer.SetBool("Run", false);
        }else if (VerificarSuelo.tocandoSuelo)
        {
            animatorPlayer.SetBool("Jump", false);
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
        /*UnityEngine.Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
        */
    }
}
