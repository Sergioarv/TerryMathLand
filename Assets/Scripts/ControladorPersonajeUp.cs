using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPersonajeUp : MonoBehaviour
{
    private float velocidadMAX = 3.6f;

    Rigidbody2D rbPlayer;
    SpriteRenderer spritePlayer;
    Animator animatorPlayer;

    Vector2 direcccion;

    private GameObject mano;

    bool mirandoDerecha;
    float movX;
    float movY;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
        mirandoDerecha = spritePlayer.flipX;
        mano = GameObject.Find("Mano");
    }

    private void Update()
    {
        movX = Input.GetAxisRaw("Horizontal");
        movY = Input.GetAxisRaw("Vertical");

        animatorPlayer.SetFloat("MovX", movX);
        animatorPlayer.SetFloat("MovY", movY);


        spritePlayer.sortingOrder = animatorPlayer.GetFloat("UltimoY") == 1 && animatorPlayer.GetFloat("UltimoX") == 0 ? 1 : 0;

        if (movX != 0 || movY != 0)
        {
            animatorPlayer.SetFloat("UltimoX", movX);
            animatorPlayer.SetFloat("UltimoY", movY);
        }

        direcccion = new Vector2(movX, movY).normalized;
    }

    private void FixedUpdate()
    {
        rbPlayer.MovePosition(rbPlayer.position + direcccion * velocidadMAX * Time.deltaTime);

        volteo();
        posicionMano();
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
        else if (movY != 0)
        {
            float vY = movY * 0.3f;
            mano.transform.localPosition = new Vector2(0, vY);
            mano.transform.GetChild(0).localPosition = new Vector2(0, vY);
            if (hijosMano > 1) mano.transform.GetChild(1).localPosition = new Vector2(0, vY);
        }
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
}
