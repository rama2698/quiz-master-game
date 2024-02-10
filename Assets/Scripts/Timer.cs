using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 10f;
    [SerializeField] float timeToShowCorrectAnswer = 2f;
    float timerValue;
    bool isAnsweringQuestion;
    public float fillFraction;
    public bool loadNextQuestion;
    void Update()
    {
        UpdateTimer();
    }

    public bool GetAnsweringQuestion(){
        return isAnsweringQuestion;
    }

    public void CancelTimer(){
        timerValue = 0f;
    }

    void UpdateTimer() {
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion) {
            if(timerValue > 0){
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        } else {
            if(timerValue > 0){
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
