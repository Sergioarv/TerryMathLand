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
    float radioVerificaSuelo = 0.2f;
    public LayerMask sueloLayer;
    public Transform verificadorSuelo;
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
            animatorPlayer.SetBool("tocandoSuelo", tocandoElSuelo);
            rbPlayer.AddForce(new UnityEngine.Vector2(0, fuerzaSalto));
        }
    }

    private void FixedUpdate()
    {
        tocandoElSuelo = Physics2D.OverlapCircle(verificadorSuelo.position, radioVerificaSuelo, sueloLayer);
        animatorPlayer.SetBool("tocandoSuelo", tocandoElSuelo);
        animatorPlayer.SetFloat("velocidadVertical", rbPlayer.velocity.y);

        float movimiento = Input.GetAxis("Horizontal");
        animatorPlayer.SetFloat("velocidad", Mathf.Abs(movimiento));
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
