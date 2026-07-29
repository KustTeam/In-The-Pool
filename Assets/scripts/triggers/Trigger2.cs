using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] // Автоматически добавит AudioSource на объект в Unity
public class Trigger2 : MonoBehaviour
{
    private Light spotLight;

    private bool isPlayerInside = false;
    private bool isPredmetInside = false;
    public GameObject poo;

    [Header("Настройки Кат-сцен")]
    [SerializeField] private ConcertManager concertManager; // Первая кат-сцена (для состояния 2)
    [SerializeField] private scen2 secondCutscene;           // Вторая кат-сцена (для состояния 6)

    [Header("Звуковые эффекты")]
    public AudioClip questSound; // Звук для состояния 3
    [SerializeField] private AudioClip state7Sound; // Звук для состояния 7
    
    private AudioSource myAudioSource;
    private bool concertStartedForState2 = false; 
    private bool concertStartedForState6 = false; 
    private bool state7SoundPlayed = false; 

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
        
        // Настраиваем AudioSource на чистый 2D-звук, чтобы его было слышно везде
        myAudioSource = GetComponent<AudioSource>();
        if (myAudioSource != null)
        {
            myAudioSource.spatialBlend = 0f; 
            myAudioSource.playOnAwake = false;
        }
    }

    private void Update() 
    {
        // Включение/выключение света по вашим состояниям
        if (TheWorld.StateStory == 1 || TheWorld.StateStory == 3 || TheWorld.StateStory == 5 || TheWorld.StateStory == 7)
        {
            spotLight.enabled = true;
        }
        else
        {
            spotLight.enabled = false;
        }

        // --- ЗАПУСКАЕМ ПЕРВУЮ КАТ-СЦЕНУ (State 2) ---
        if (TheWorld.StateStory == 2 && !concertStartedForState2)
        {
            concertStartedForState2 = true; 
            if (concertManager != null) concertManager.StartConcert(); 
        }

        // --- ЗАПУСКАЕМ ВТОРУЮ КАТ-СЦЕНУ (State 6) ---
        if (TheWorld.StateStory == 6 && !concertStartedForState6)
        {
            concertStartedForState6 = true; 
            if (secondCutscene != null) secondCutscene.StartConcert(); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TheWorld.StateStory == 1 || TheWorld.StateStory == 5)
            {
                TheWorld.StateStory += 1;
            }
        }

        if (other.CompareTag("Player")) isPlayerInside = true;
        if (other.CompareTag("predmet")) isPredmetInside = true;

        // Проверка на то, что игрок принес предмет в зону
        if (isPlayerInside && isPredmetInside)
        {
            if (TheWorld.StateStory == 3)
            {
                if (questSound != null && myAudioSource != null)
                {
                    myAudioSource.PlayOneShot(questSound);
                }
                TheWorld.StateStory = 4;
                Destroy(poo);
                isPredmetInside = false;
                isPlayerInside = false;
            }
            // Если игрок принес предмет на состоянии 6 (после второй кат-сцены)
            else if (TheWorld.StateStory == 6)
            {
                TheWorld.StateStory = 7; 

                // Включаем звук седьмого состояния
                if (state7Sound != null && !state7SoundPlayed && myAudioSource != null)
                {
                    myAudioSource.PlayOneShot(state7Sound);
                    state7SoundPlayed = true; 
                }
            }
            else if (TheWorld.StateStory == 7)
            {
                TheWorld.StateStory = 8;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInside = false;
        if (other.CompareTag("predmet")) isPredmetInside = false;
    }
}
