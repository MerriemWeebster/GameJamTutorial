using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private float speed, jump;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        if(hor != 0f)
        {
            rigidbody2D.velocity = new Vector2(hor * speed, rigidbody2D.velocity.y);
        }

        if(Input.GetButtonDown("Jump"))
        {
            rigidbody2D.velocity += Vector2.up * jump;
        }
    }
}
