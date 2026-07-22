using UnityEngine;

public class take_up_down : MonoBehaviour
{
    public float pickupRange = 4f;
    public Transform holdPoint;
    public GameObject pressUI;

    private Camera cam;

    private GameObject targetObject;
    private Rigidbody heldRb;
    private Collider heldCollider;
    private Light currentLight;

    private float checkTimer = 0f;
    public float checkDelay = 0.1f;

    void Start()
    {
        cam = Camera.main;

        if (pressUI != null)
            pressUI.SetActive(false);
    }

    void Update()
    {
        checkTimer += Time.deltaTime;

        if (checkTimer >= checkDelay)
        {
            DetectObject();
            checkTimer = 0f;
        }

        if (!Input.GetKeyDown(KeyCode.E))
            return;

        if (heldRb == null)
        {
            if (targetObject != null)
                Pickup(targetObject);
        }
        else
        {
            Drop();
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
            if (currentLight != null)
                currentLight.enabled = false;

            targetObject = newTarget;

            if (targetObject != null)
            {
                currentLight = targetObject.GetComponentInChildren<Light>();

                if (currentLight != null)
                    currentLight.enabled = true;
            }
            else
            {
                currentLight = null;
            }
        }

        if (pressUI != null)
            pressUI.SetActive(targetObject != null && heldRb == null);
    }

    void Pickup(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb == null)
            return;

        heldRb = rb;
        heldCollider = obj.GetComponent<Collider>();

        if (heldCollider != null)
            heldCollider.enabled = false;

        heldRb.useGravity = false;
        heldRb.linearVelocity = Vector3.zero;

        obj.transform.SetParent(holdPoint);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }

    void Drop()
    {
        heldRb.transform.SetParent(null);

        if (heldCollider != null)
            heldCollider.enabled = true;

        heldRb.useGravity = true;
        heldRb.AddForce(cam.transform.forward * 2f, ForceMode.Impulse);

        heldRb = null;
        heldCollider = null;
    }
}