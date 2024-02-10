using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredAlready = true;

    [Header("Button Images")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    public bool quizCompleted; 
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion){
            hasAnsweredAlready = false;
            NextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredAlready && !timer.GetAnsweringQuestion()){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }
    public void OnAnswerSelection(int index){
        hasAnsweredAlready = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        
        if(progressBar.value == progressBar.maxValue){
            quizCompleted = true;
        }
    }

    void NextQuestion(){
        if(questions.Count > 0){
            SetButtonState(true);
            SetDefaultAnswerSprite();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.SetQuestionsSeen();
        }
    }

    void GetRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion(){
        questionText.text = currentQuestion.getQuestion();

        for(int i=0; i < 4; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
        }

    }

    void DisplayAnswer(int index){
        Image buttonImg;
        if(index == currentQuestion.getCorrectAnswerIndex()){
            questionText.text = "Correct!\n"+ currentQuestion.getAnswer(currentQuestion.getCorrectAnswerIndex()) + " is the right answer.";
            buttonImg = answerButtons[index].GetComponent<Image>();
            buttonImg.sprite = correctAnswerSprite;
            scoreKeeper.SetCorrectAnswer();
        }
        else {
            correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
            string correctAnswer = currentQuestion.getAnswer(correctAnswerIndex);
            questionText.text = "Sorry! the correct answer is\n" + correctAnswer;
            buttonImg = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImg.sprite = correctAnswerSprite;
        }
    }

    void SetButtonState(bool state){
        for(int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state; 
        }
    }

    void SetDefaultAnswerSprite(){
        for(int i = 0; i < answerButtons.Length; i++) {
            Image buttonImg = answerButtons[i].GetComponent<Image>();
            buttonImg.sprite = defaultAnswerSprite;
        }
    }
}
