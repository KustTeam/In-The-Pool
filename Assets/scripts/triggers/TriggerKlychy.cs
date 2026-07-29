using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables; // Обязательно для PlayableDirector

public class TriggerKlychy : MonoBehaviour
{
    private Light spotLight;

    [Header("Настройки UI для Состояния 4")]
    [SerializeField] private GameObject ImageToShow; 

    [Header("Скрипты управления игрока")]
    [SerializeField] private WASD movementScript; 
    [SerializeField] private camera mouseLookScript; 
    [SerializeField] private GameObject playerObject; // Сам объект игрока для CharacterController

    [Header("Настройки Кат-сцены")]
    [SerializeField] private PlayableDirector cameraDirector;

    private CharacterController charController;
    private bool isImageCoroutineRunning = false;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();

        if (ImageToShow != null)
        {
            ImageToShow.SetActive(false);
        }

        // Находим контроллер игрока, если объект назначен
        if (playerObject != null)
        {
            charController = playerObject.GetComponent<CharacterController>();
        }
    }

    // Подписываемся на событие окончания кат-сцены при активации скрипта
    private void OnEnable()
    {
        if (cameraDirector != null) cameraDirector.stopped += OnCutsceneEnded;
    }

    // Отписываемся при выключении, чтобы избежать утечек памяти
    private void OnDisable()
    {
        if (cameraDirector != null) cameraDirector.stopped -= OnCutsceneEnded;
    }

    private void Update() 
    {
        if (TheWorld.StateStory == 0)
        {
            spotLight.enabled = true;
        }
        else if (TheWorld.StateStory == 4)
        {
            spotLight.enabled = true;
        }
        else
        {
            spotLight.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TheWorld.StateStory == 0)
            {
                TheWorld.StateStory += 1;
            }
            else if (TheWorld.StateStory == 4 && !isImageCoroutineRunning)
            {
                StartCoroutine(ShowImageBeforeState5());
            }
        }
    }

    private IEnumerator ShowImageBeforeState5()
    {
        isImageCoroutineRunning = true;

        // 1. ОТКЛЮЧАЕМ ХОДЬБУ И ПОВОРОТ КАМЕРЫ ИГРОКА
        if (movementScript != null) movementScript.enabled = false;
        if (mouseLookScript != null) mouseLookScript.enabled = false;
        if (charController != null) charController.enabled = false;

        // 2. Включаем картинку на Canvas
        if (ImageToShow != null)
        {
            ImageToShow.SetActive(true);
        }

        // 3. Ждем ровно 2 секунды
        yield return new WaitForSeconds(2f);

        // 4. Выключаем картинку
        if (ImageToShow != null)
        {
            ImageToShow.SetActive(false);
        }

        // 5. Переключаем состояние мира на 5
        TheWorld.StateStory = 5;

        // 6. ЗАПУСКАЕМ КАТ-СЦЕНУ
        if (cameraDirector != null)
        {
            cameraDirector.Play();
        }
        else
        {
            Debug.LogError("Забыли перетащить PlayableDirector в поле Camera Director!");
            // Если кат-сцены нет, сразу возвращаем управление, чтобы игра не зависла
            ResetPlayerControl();
        }

        isImageCoroutineRunning = false;
    }

    // ЭТОТ МЕТОД СРАБОТАЕТ АВТОМАТИЧЕСКИ, КОГДА TIMELINE ДОЙДЕТ ДО КОНЦА
    private void OnCutsceneEnded(PlayableDirector obj)
    {
        ResetPlayerControl();
    }

    // Метод для безопасного возвращения управления игроку
    private void ResetPlayerControl()
    {
        if (charController != null) charController.enabled = true;
        if (movementScript != null) movementScript.enabled = true;
        if (mouseLookScript != null) mouseLookScript.enabled = true;
    }
}
