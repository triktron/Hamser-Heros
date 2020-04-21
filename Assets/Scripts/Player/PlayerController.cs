using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed = 100;

    public Vector2 DragSpeed;
    public float GroundScanLength;
    public LayerMask SlippyLayers;

    Vector3 Movement;
    Rigidbody rb;
    CharacterController cc;

    public bool IsOnGround;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float angle = Camera.main.transform.rotation.eulerAngles.y;

        Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        Vector3 left = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, -Mathf.Sin(Mathf.Deg2Rad * angle));

        Debug.DrawRay(transform.position, forward * 2, Color.red);
        Debug.DrawRay(transform.position, left * 2, Color.green);
        Debug.DrawRay(transform.position, Movement * 2, Color.magenta);
        Debug.DrawRay(transform.position, Vector3.down * GroundScanLength);
    }

    public void Move(InputAction.CallbackContext context)
    {
        float angle = Camera.main.transform.rotation.eulerAngles.y;

        var input = context.ReadValue<Vector2>();

        Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        Vector3 left = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, -Mathf.Sin(Mathf.Deg2Rad * angle));

        Movement = input.x * left + input.y * forward;
    }

    bool IsOnLayerMask(int layer, LayerMask mask)
    {
        return mask == (mask | (1 << layer));
    }


    void FixedUpdate()
    {
        RaycastHit info;
        IsOnGround = Physics.Raycast(transform.position, Vector3.down, out info, GroundScanLength);

        //rb.drag = (Movement == Vector3.zero && !IsOnLayerMask(info.collider.gameObject.layer, SlippyLayers)) ? DragSpeed.x : DragSpeed.y;

        if (IsOnGround) rb.AddForce(Movement * Speed);
    }
}
