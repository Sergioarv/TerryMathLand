using System.Collections;
using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    private float velocidadMAX = 3.6f;
    private float fuerzaSalto = 4f;

    Rigidbody2D rbPlayer;
    SpriteRenderer spritePlayer;
    Animator animatorPlayer;

    bool mirandoDerecha;

    float movX;
    float dirX;

    private GameObject mano;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
        mano = GameObject.Find("Mano");
        mirandoDerecha = spritePlayer.flipX;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        movX = Input.GetAxisRaw("Horizontal");
        animatorPlayer.SetFloat("MovX", movX);
        if(mano.GetComponent<ManoLanzadora>() != null) mano.GetComponent<ManoLanzadora>().dirX = movX;

        if (movX != 0)
        {
            rbPlayer.velocity = new Vector2(movX * velocidadMAX + Time.deltaTime, rbPlayer.velocity.y);
            animatorPlayer.SetBool("Run", true);
            animatorPlayer.SetFloat("UltimoX", movX);
            dirX = movX;
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
        }else if (VerificarSuelo.tocandoOpcion)
        {
            animatorPlayer.SetBool("Jump", false);
        }

        volteo();
        posicionMano();
    }

    public IEnumerator congelarMovimiento()
    {
        velocidadMAX = 0;
        fuerzaSalto = 0;
        yield return new WaitForSeconds(1f);
        velocidadMAX = 3.6f;
        fuerzaSalto = 4f;
        animatorPlayer.SetBool("Tirar", false);
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
            mano.transform.GetChild(0).localPosition = vX < 0 ? new Vector2(0.16f, 0f) : new Vector2(0.56f, 0f);
            if (hijosMano > 1)
            {
                mano.transform.GetChild(1).gameObject.GetComponent<OpcionLanzable>().dirX = dirX;
                mano.transform.GetChild(1).localPosition = vX < 0 ? new Vector2(0.16f, 0f) : new Vector2(0.56f, 0f);
            }
        }
    }

    public void detenerLanzamiento()
    {
        animatorPlayer.SetBool("Tirar", true);
    }
}
