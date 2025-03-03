using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    [SerializeField]
    public Animator animator;


    [SerializeField]
    public float speed;
    public float jump;

    [SerializeField] private BoxCollider2D boxCol;



    private Rigidbody2D rb;
    //Collider Variables
    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;
    // Start is called before the first frame update
    void Start()
    {
        //Fetching initial collider properties
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxis("Jump");


        PlayJumpAnimation(Vertical);
        MoveCharacter(Horizontal,Vertical);

        PlayMovementAnimation(Horizontal);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }

    private void PlayMovementAnimation(float Horizontal)
    {
        animator.SetFloat("Speed", Mathf.Abs(Horizontal));

        Vector3 scale = transform.localScale;

        if (Horizontal < 0f)
        {
            scale.x = -1.0f * Mathf.Abs(scale.x);
        }
        else if (Horizontal > 0f)
        {

            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private void MoveCharacter(float Horizontal,float vertical)
    {
        Vector3 position = transform.position;
        position.x += Time.deltaTime * Horizontal * speed;
        transform.position = position;

        if (vertical > 0)
        {
            rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
    }

    public void Crouch(bool crouch)
    {
        if (crouch == true)
        {
            float offX = -0.0978f;     //Offset X
            float offY = 0.5947f;      //Offset Y

            float sizeX = 0.6988f;     //Size X
            float sizeY = 1.3398f;     //Size Y

            boxCol.size = new Vector2(sizeX, sizeY);   //Setting the size of collider
            boxCol.offset = new Vector2(offX, offY);   //Setting the offset of collider
        }
        else
        {
            //Reset collider to initial values
            boxCol.size = boxColInitSize;
            boxCol.offset = boxColInitOffset;
        }
        animator.SetBool("Crouch", crouch);
    }

    public void PlayJumpAnimation(float vertical)
    {
        if (vertical > 0)
        {
            animator.SetTrigger("Jump");
        }
    }
}