using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public Rigidbody RigBB;
    public float sPeed;
    public float horizontalInput;
    public float verticalInput;
    private Vector3 direction;
    public Quaternion camQuart;
    public Vector3 vecSaMere;
    public Animator animgogol;
    private bool isOnMove;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
      camQuart = camQuart.normalized;
       
    }

    // Update is called once per frame
    void Update()
    {
        bool isOnMove = false;
        animgogol.SetBool("isMoving", false);
        animgogol.SetBool("stealthy", false);
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Comment transformer les deux float en vecteur de direction ?
        //Pour “Vertical”, on sait qu’on veut aller vers l’avant quand on pousse le stick vers le haut

        direction = (transform.right * horizontalInput + transform.forward * verticalInput) * sPeed;
        Debug.DrawRay(transform.position, direction, Color.blue);
        
        camQuart = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
        camQuart.eulerAngles = new Vector3(0, camQuart.eulerAngles.y, camQuart.eulerAngles.z);

     

        if (direction.magnitude > 0)
        {
            animgogol.SetBool("isMoving", true);
            isOnMove = true;
        }
        if (direction.magnitude > 2)
        {
           animgogol.SetFloat("speedy", 1 );
        }

        //Quaternion.LookRotation(direction, direction.upwards = Vector3.up);
        //player.Quaternion.LookRotation = vector 3 camera .LookAt horizontal
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = rotation;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation);

        if (Input.GetKeyDown(KeyCode.JoystickButton4) && isOnMove)
        {
            animgogol.SetBool("steatlhy", true);
            isOnMove = true;
        }
    }
    private void FixedUpdate()
    {
        // annuler effet rigidbody.velocity sur Vector3.y (aka direction.y)
        direction.y = RigBB.velocity.y;
        RigBB.velocity = direction;
        RigBB.MoveRotation(camQuart);
        //convert euler.x into quaternion

      
      
    }

}
