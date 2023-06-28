using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARTraining : MonoBehaviour
{
    private int ticks;
    private float timer;

    [Header("Objects")]
    private int currentState = 0;
    [SerializeField] private GameObject[] stages;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private TextMeshProUGUI finalText;
    private bool LastStage => currentState == stages.Length - 1;

    [Header("Water Level")]
    private float waterLevel = 1;
    [SerializeField] private float waterToleranceLevel;
    [SerializeField] private Slider waterSlider;
    private bool HasWater => waterLevel >= waterToleranceLevel;

    [Header("Pesticides")]
    private float healthLevel = 1;
    [SerializeField] private float healthToleranceLevel;
    [SerializeField] private Slider healthSlider;
    private bool IsHealthy => healthLevel >= healthToleranceLevel;

    bool isDead = false;

    private void Start()
    {
        waterLevel = healthLevel = 1;
    }

    public void NextStage()
    {
        if (LastStage)
        {
            playerUI.SetActive(false);
            finalText.gameObject.SetActive(true);
            finalText.text = "Nice job!";
            return;
        }

        stages[currentState].SetActive(false);

        currentState++;
        stages[currentState].SetActive(true);
    }

    private void Update()
    {
        if (LastStage || isDead) return;

        waterLevel -= Time.deltaTime / 20f;
        waterSlider.value = waterLevel;

        healthLevel -= Time.deltaTime / 30f;
        healthSlider.value = healthLevel;


        if (timer < 1)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                ticks++;
                if (ticks >= 10)
                {
                    if (IsHealthy && HasWater)
                    {
                        NextStage();
                        ticks = 0;
                    }
                    else
                    {
                        string errorMessage = "";
                        errorMessage += !HasWater ? "\nNeeds water" : "";
                        errorMessage += !IsHealthy ? "\nNeeds pesticide" : "";
                        finalText.gameObject.SetActive(true);
                        finalText.text = errorMessage;
                        isDead = true;
                    }
                }
            }
        }
    }

    public void Water()
    {
        waterSlider.value = waterLevel = 1;
    }

    public void Health()
    {
        healthSlider.value = healthLevel = 1;
    }
}
