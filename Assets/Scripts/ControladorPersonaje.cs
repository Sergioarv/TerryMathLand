using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPersonaje : MonoBehaviour //<>
{
    public float velocidadMAX;
    Rigidbody2D rbPlayer;
    Animator animatorPlayer;
    bool mirandoDerecha;

    bool tocandoElSuelo = false;
    public LayerMask sueloLayer;
    public float fuerzaSalto;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();

        mirandoDerecha = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tocandoElSuelo && Input.GetAxis("Jump") > 0) 
        {
            tocandoElSuelo = false;
            rbPlayer.AddForce(new UnityEngine.Vector2(0, fuerzaSalto));
        }
    }

    private void FixedUpdate()
    {

        float movimiento = Input.GetAxis("Horizontal");
        rbPlayer.velocity = new UnityEngine.Vector2(movimiento * velocidadMAX, rbPlayer.velocity.y);


        if (movimiento > 0 && !mirandoDerecha) 
        {
            volteo();
        }
        else if (movimiento < 0 && mirandoDerecha)
        {
            volteo();
        }
    }

    void volteo() 
    {
        mirandoDerecha = !mirandoDerecha;
        UnityEngine.Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Opcion"))
        {
            Debug.Log("Respondio" + collision.name);
            GameManagerGeneric gameManagerGeneric = FindObjectOfType<GameManagerGeneric>();

            gameManagerGeneric.cargarEscena();
        }
    }
}
