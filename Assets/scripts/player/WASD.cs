using UnityEngine;

public class WASD : MonoBehaviour
{
    public float speed = 5f;

    private GameObject cam;
    private float yRotation;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (cam != null)
        {
            yRotation = cam.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ);
        if (move.magnitude > 0.01f)
        {
            // Rotate movement vector by camera Y rotation
            move = Quaternion.Euler(0f, yRotation, 0f) * move;
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }
    }
}