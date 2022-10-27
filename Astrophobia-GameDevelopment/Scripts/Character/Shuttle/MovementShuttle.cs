using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShuttle : MonoBehaviour
{
    public float forwardSpeed = 25f;
    public float strafeSpeed = 7.5f;
    public float hoverSpeed = 5f;

    private float activeForward;
    private float activeStrafe;
    private float activeHover;

    public float forwardAccel = 2.5f;
    public float strafeAccel = 2f;
    public float hoverAccel = 2f;

    public float lookRollSpeed = 90f;
    public Vector2 lookInput, ScreenCenter, mouseDistance;

    public float roll = 0.0f;
    public float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    private int LayerGround;
    public float landInput;


    void Start()
    {
        ScreenCenter.x = Screen.width * .5f;
        ScreenCenter.y = Screen.height * .5f;
        LayerGround = LayerMask.NameToLayer("Ground");
        Cursor.lockState = CursorLockMode.Confined;

    }

    void Update()
    {
        landInput = Input.GetAxis("Landing");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100) && hit.transform.gameObject.tag == "Planet")
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 100, Color.yellow);
            if (landInput > 0)
            {
                Debug.Log(hit.distance);
                transform.position -= transform.TransformDirection(Vector3.down) * -hit.distance * Time.deltaTime;
                landInput = 0;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 100, Color.white);
        }

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - ScreenCenter.x) / ScreenCenter.y;
        mouseDistance.y = (lookInput.y - ScreenCenter.y) / ScreenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        roll = Input.GetAxisRaw("Roll") * rollAcceleration;
        transform.Rotate(0.0f, 0.0f, roll * Time.deltaTime * rollAcceleration, Space.Self);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
        transform.Rotate(-mouseDistance.y * lookRollSpeed * Time.deltaTime, mouseDistance.x * lookRollSpeed * Time.deltaTime, 0f, Space.Self);


        activeForward = Mathf.Lerp(activeForward, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAccel * Time.deltaTime);
        activeStrafe = Mathf.Lerp(activeStrafe, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAccel * Time.deltaTime);
        activeHover = Mathf.Lerp(activeHover, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAccel * Time.deltaTime);

        transform.position += transform.forward * activeForward * Time.deltaTime;
        transform.position += (transform.right * activeStrafe * Time.deltaTime) + (transform.up * activeHover * Time.deltaTime);
    }
}
