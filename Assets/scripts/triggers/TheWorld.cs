using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheWorld : MonoBehaviour
{
    public static int StateStory = 0;
    public int StateStory_1;
    public TMP_Text taskText;
    public GameObject kluch;
    public GameObject suchoc;
    public GameObject swabra;
    public GameObject blod;
    public AudioClip KluchSound;

    void Start()
    {
        suchoc.SetActive(false);
        swabra.SetActive(false);
        blod.SetActive(false);
        StateStory = StateStory_1;
        kluch = GameObject.FindWithTag("kluch");
        if (kluch != null)
        {
            kluch.SetActive(false);
        }
        taskText.text = "Положи ключи";

    }


    void Update()
    {
        if (StateStory_1 != StateStory)
        {
            ChangeTask();
            StateStory_1 = StateStory;
        }
    }


    void ChangeTask()
    {
        if (StateStory == 0)
        {
            taskText.text = "Положи ключи";
        }
        if (StateStory == 1)
        {
            AudioSource.PlayClipAtPoint(KluchSound, transform.position);
            kluch.SetActive(true);
            taskText.text = "Проверь всё";
        }
        if (StateStory == 2)
        {
            suchoc.SetActive(true);
            taskText.text = "Иди в коморку и возьми инструмент";
        }
        if (StateStory == 3)
        {
            taskText.text = "УБЕРИСЬ";
        }
        if (StateStory == 4)
        {
            Destroy(suchoc);
            taskText.text = "Отдохни";
        }
        if (StateStory == 5)
        {
            blod.SetActive(true);
            taskText.text = "Проверь что там";
        }
        if (StateStory == 6)
        {
            swabra.SetActive(true);
            taskText.text = "забери швабру";
        }
        if (StateStory == 7)
        {
            blod.SetActive(false);
            taskText.text = "Назад";
        }
        if (StateStory == 8)
        {
            Destroy(swabra);
            taskText.text = "Проверь что за звуки";
        }
    }
}