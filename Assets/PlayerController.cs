using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    bool isCrouched =false;
    [SerializeField]
    public Animator animator;

    [SerializeField] private BoxCollider2D boxCol;

    //Collider Variables
    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;
    // Start is called before the first frame update
    void Start()
    {
        //Fetching initial collider properties
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        float speed = Input.GetAxisRaw("Horizontal");
        
        animator.SetFloat("Speed", Mathf.Abs(speed));
        PlayJumpAnimation(VerticalInput);


        Vector3 scale = transform.localScale;

        if(speed < 0f)
        {
            scale.x= -1.0f * Mathf.Abs(scale.x);
        }
        else if(speed > 0f) {
        
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
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