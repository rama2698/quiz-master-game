using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswer = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswer(){
        return correctAnswer;
    }

    public int GetQuestionsSeen(){
        return questionsSeen;
    }

    public void SetCorrectAnswer(){
        correctAnswer += 1;
    }

    public void SetQuestionsSeen(){
        questionsSeen += 1;
    }

    public int CalculateScore(){
        return Mathf.RoundToInt(correctAnswer/(float)questionsSeen * 100);
    }
}
