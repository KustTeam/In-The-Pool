using UnityEngine;

// Этот атрибут автоматически добавит компонент AudioSource на камеру, если его там нет
[RequireComponent(typeof(AudioSource))]
public class camera : MonoBehaviour 
{ 
    public float mouseSensitivity = 200f; 
    float xRotation = 0f; 

    [Header("Настройки звука сюжета")]
    [SerializeField] private AudioClip state7Sound; // Сюда перетащите аудиофайл в инспекторе
    
    private AudioSource audioSource;
    private bool state7SoundPlayed = false; // Флаг, чтобы звук не запускался каждый кадр в Update

    void Start() 
    { 
        Cursor.lockState = CursorLockMode.Locked; 

        // Находим и настраиваем AudioSource прямо при старте
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.spatialBlend = 0f; // Делаем звук плоским 2D, чтобы его было идеально слышно
            audioSource.playOnAwake = false;
        }
    } 

    void Update() 
    { 
        // --- БЛОК ВРАЩЕНИЯ КАМЕРЫ ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; 

        xRotation -= mouseY; 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        // Крутим камеру вверх/вниз
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        
        // Крутим ИГРОКА влево/вправо (чтобы WASD понимал, где перед)
        if (transform.parent != null)
        {
            transform.parent.Rotate(Vector3.up * mouseX); 
        }

        // --- БЛОК ПРОИГРЫВАНИЯ ЗВУКА НА СОСТОЯНИИ 7 ---
        // Каждый кадр проверяем состояние мира. Если оно равно 7 и звук еще не играл — включаем его
        if (TheWorld.StateStory == 7 && !state7SoundPlayed)
        {
            if (state7Sound != null && audioSource != null)
            {
                audioSource.PlayOneShot(state7Sound); // Включаем звук на полную громкость [health]
                state7SoundPlayed = true;             // Фиксируем запуск, чтобы звук больше не повторялся
                Debug.Log("Звук состояния 7 успешно воспроизведен через скрипт камеры!");
            }
        }

        // Сбрасываем флаг, если состояние изменилось, чтобы скрипт мог работать корректно в будущем
        if (TheWorld.StateStory != 7 && state7SoundPlayed)
        {
            state7SoundPlayed = false;
        }
    } 
}
