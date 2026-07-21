using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheWorld : MonoBehaviour
{
    public static int StateStory = 0;
    private int StateStory_1;
    public TMP_Text taskText;

    void Start()
    {
        StateStory = StateStory_1;
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
            taskText.text = "Проверь всё";
        }
        if (StateStory == 2)
        {
            taskText.text = "Иди в коморку и возьми инструмент";
        }
        if (StateStory == 3)
        {
            taskText.text = "УБЕРИСЬ";
        }
        if (StateStory == 4)
        {
            taskText.text = "Отдохни";
        }
        if (StateStory == 5)
        {
            taskText.text = "Проверь что там";
        }
        if (StateStory == 6)
        {
            taskText.text = "забери швабру";
        }
        if (StateStory == 7)
        {
            taskText.text = "Назад";
        }
        if (StateStory == 8)
        {
            taskText.text = "Проверь что за звуки";
        }
    }
}