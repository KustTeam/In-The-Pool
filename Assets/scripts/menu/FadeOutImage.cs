using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeOutImage : MonoBehaviour
{
    [Header("Настройки времени")]
    [Tooltip("Задержка перед началом исчезновения (в секундах)")]
    [SerializeField] private float delayBeforeStart = 0.5f;
    
    [Tooltip("Длительность самого исчезновения (в секундах)")]
    [SerializeField] private float fadeDuration = 2.0f;

    [Header("Дополнительно")]
    [Tooltip("Уничтожить объект после полного исчезновения?")]
    [SerializeField] private bool destroyOnComplete = true;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        // Автоматически получаем компонент CanvasGroup с этого же объекта
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        // Принудительно делаем объект полностью видимым в самом начале
        canvasGroup.alpha = 1f;

        // Запускаем корутину плавного исчезновения
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        // 1. Ждем указанное время перед стартом
        yield return new WaitForSeconds(delayBeforeStart);

        float currentTime = 0f;

        // 2. Плавно уменьшаем альфу каждый кадр
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            // Рассчитываем текущую прозрачность от 1 до 0
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            yield return null; 
        }

        // Гарантируем, что в конце альфа точно равна 0
        canvasGroup.alpha = 0f;

        // 3. Финальное действие
        if (destroyOnComplete)
        {
            Destroy(gameObject);
        }
        else
        {
            // Если не удаляем, то отключаем взаимодействие, чтобы картинка не блокировала клики мышкой
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
