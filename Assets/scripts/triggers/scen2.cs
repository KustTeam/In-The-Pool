using UnityEngine;
using UnityEngine.Playables;

public class scen2 : MonoBehaviour
{
    [Header("Компоненты Timeline")]
    [SerializeField] private PlayableDirector director; 

    [Header("Персонаж и управление")]
    [SerializeField] private GameObject player;
    [SerializeField] private WASD movementScript; 
    [SerializeField] private camera mouseLookScript; 

    [Header("Финальные настройки (Телепорт и Поворот)")]
    [SerializeField] private bool teleportAtEnd = true; // Включить/выключить телепорт
    [SerializeField] private Vector3 finalPosition = new Vector3(0f, 0f, 0f); // Координаты перемещения
    [SerializeField] private Vector3 finalRotationEuler = new Vector3(0f, -90f, 0f); // Углы поворота

    private CharacterController charController;

    private void Start()
    {
        // Находим CharacterController, если он есть, чтобы телепорт сработал без багов
        if (player != null)
        {
            charController = player.GetComponent<CharacterController>();
        }
    }

    private void OnEnable()
    {
        if (director != null) director.stopped += OnConcertEnded;
    }

    private void OnDisable()
    {
        if (director != null) director.stopped -= OnConcertEnded;
    }

    public void StartConcert()
    {
        if (director == null) return;

        // 1. Отключаем ходьбу и мышь
        if (movementScript != null) movementScript.enabled = false;
        if (mouseLookScript != null) mouseLookScript.enabled = false;

        // 2. Запускаем концерт
        director.Play();
    }

    private void OnConcertEnded(PlayableDirector obj)
    {
        // 3. Перемещаем и поворачиваем игрока
        if (player != null)
        {
            // Отключаем контроллер на миллисекунду, иначе Unity проигнорирует смену координат
            if (charController != null) charController.enabled = false;

            if (teleportAtEnd)
            {
                player.transform.position = finalPosition;
            }

            player.transform.rotation = Quaternion.Euler(finalRotationEuler);

            // Возвращаем контроллер в рабочее состояние
            if (charController != null) charController.enabled = true;
        }

        // 4. Возвращаем управление игроку
        if (movementScript != null) movementScript.enabled = true;
        if (mouseLookScript != null) mouseLookScript.enabled = true;
    }
}
