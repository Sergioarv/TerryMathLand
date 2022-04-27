using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPersonajeUp : MonoBehaviour
{
    public float velocidadMAX = 3.6f;

    Rigidbody2D rbPlayer;
    SpriteRenderer spritePlayer;
    Animator animatorPlayer;

    Vector2 direcccion;

    bool mirandoDerecha;
    float movX;
    float movY;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
        mirandoDerecha = spritePlayer.flipX;
    }

    private void Update()
    {
        movX = Input.GetAxisRaw("Horizontal");
        movY = Input.GetAxisRaw("Vertical");

        animatorPlayer.SetFloat("MovX", movX);
        animatorPlayer.SetFloat("MovY", movY);

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
