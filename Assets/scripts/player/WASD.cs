using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WASD : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        // Автоматически включаем защиту от бешеного кручения стен в коде
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Получаем ввод
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Двигаемся относительно НАПРАВЛЕНИЯ ИГРОКА (куда он повернут мышкой)
        Vector3 move = (transform.forward * moveZ) + (transform.right * moveX);

        // Ограничиваем скорость по диагонали
        if (move.magnitude > 1f) move.Normalize();

        // Плавно меняем скорость Rigidbody без рывков и прохождения сквозь стены
        rb.linearVelocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);
    }
}
