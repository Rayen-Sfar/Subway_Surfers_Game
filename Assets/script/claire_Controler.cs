using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claire_Controler : MonoBehaviour
{
    Animator anim;
    public float moveSpeed = 5.0f; // Vitesse de déplacement
    public float rotationSpeed = 100.0f; // Vitesse de rotation

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJump");
        }
        if (translation != 0)
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isIdle", true);
        }

    }
}
