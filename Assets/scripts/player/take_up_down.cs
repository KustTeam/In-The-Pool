using UnityEngine;

public class take_up_down : MonoBehaviour
{
    [Header("Настройки дистанции")]
    public float pickupRange = 4f;
    public Transform holdPoint;
    public float lerpSpeed = 10f; // Скорость дотягивания предмета к руке

    [Header("Интерфейс и эффекты")]
    public GameObject pressUI;
    public float checkDelay = 0.1f;

    private Camera cam;
    private GameObject targetObject;
    private Rigidbody heldRb;
    private Light currentLight;
    private float checkTimer = 0f;

    void Start()
    {
        cam = Camera.main;
        if (pressUI != null) pressUI.SetActive(false);
    }

    void Update()
    {
        // Оптимизированный таймер луча
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkDelay)
        {
            DetectObject();
            checkTimer = 0f;
        }

        // Логика нажатия клавиши
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldRb == null)
            {
                if (targetObject != null) Pickup(targetObject);
            }
            else
            {
                Drop();
            }
        }
    }

    void FixedUpdate()
    {
        // Физическое перемещение объекта за точкой удержания
        if (heldRb != null)
        {
            Vector3 targetPosition = holdPoint.position;
            heldRb.linearVelocity = (targetPosition - heldRb.transform.position) * lerpSpeed;
        }
    }

    void DetectObject()
    {
        RaycastHit hit;
        GameObject newTarget = null;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("predmet"))
            {
                newTarget = hit.collider.gameObject;
            }
        }

        if (newTarget != targetObject)
        {
            if (currentLight != null) currentLight.enabled = false;
            targetObject = newTarget;

            if (targetObject != null)
            {
                currentLight = targetObject.GetComponentInChildren<Light>();
                if (currentLight != null) currentLight.enabled = true;
            }
            else
            {
                currentLight = null;
            }
        }

        if (pressUI != null) pressUI.SetActive(targetObject != null && heldRb == null);
    }

    void Pickup(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;

        heldRb = rb;
        heldRb.useGravity = false;
        
        // Ограничиваем лишнее вращение при переносе
        heldRb.angularDamping = 5f; 
    }

    void Drop()
    {
        if (heldRb == null) return;

        heldRb.useGravity = true;
        heldRb.angularDamping = 0.05f; // Возвращаем стандартное затухание вращения
        
        // Импульс броска вперед
        heldRb.AddForce(cam.transform.forward * 3f, ForceMode.Impulse);
        
        heldRb = null;
    }
}
