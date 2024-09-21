using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float playerSpeed = 1.5f;

    public Rigidbody2D body;
    [SerializeField] private bool InAir;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (InAir)
        {
            Debug.Log (InAir);
        }
        else {

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 direction= new Vector2(xInput, 0);
        body.velocity = direction *playerSpeed;
        if (yInput >0){
            Debug.Log (yInput);
            InAir= true;
            body.velocity = direction *playerSpeed *2;
        }
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
     { 
        Debug.Log (InAir);
        if(!InAir){return;} //checks if player is in the air to continue 
        if (collision.gameObject.layer==LayerMask.NameToLayer("Floor"))//if coliided object is equal to floor 
        {
            InAir = false;
            Debug.Log (InAir);
        }
     }
}
