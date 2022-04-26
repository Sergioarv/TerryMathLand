using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    public float velocidadMAX = 3.6f;
    Rigidbody2D rbPlayer;
    Vector2 direccion;

    Animator animatorPlayer;
    
    bool tocandoElSuelo = false;
    
    public float fuerzaSalto;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
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

        rbPlayer.MovePosition(rbPlayer.position + direccion * velocidadMAX * Time.fixedDeltaTime);
        /*
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
        */
    }

    void volteo() 
    {
        /*
        mirandoDerecha = !mirandoDerecha;
        UnityEngine.Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
        */
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
