using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int Plant;
    public int Money = 3;
    public int ReadyPlant;
    public int SoldPlant;

    public TextMeshProUGUI PlantedText, HarvestedText, SoldText, MoneyText;
    public Slider PlantSlider, HarvestSlider;

    public float timerPlant = 0;
    public float timerMaxPlant = 5;
    public bool timerActivePlant = false;
    private bool isSliderCompletePlant = true;

    public float timerHarvest = 0;
    public float timerMaxHarvest = 5;
    public bool timerActiveHarvest = false;
    private bool isSliderCompleteHarvest = true;

    private bool plantingInProgress = false; // Ekim iþlemi sürüyor mu?

    private void Start()
    {
        MoneyText.text = Money.ToString() + " MONEE";
    }

    private void Update()
    {
        TimerPlant();
        TimerHarvest();
        UpdateUI();
    }

    public void TimerPlant()
    {
        if (timerActivePlant && !isSliderCompletePlant)
        {
            timerPlant += Time.deltaTime;
            float normalizedTime = timerPlant / timerMaxPlant;
            PlantSlider.value = normalizedTime;
            if (timerPlant >= timerMaxPlant)
            {
                timerPlant = 0;
                timerActivePlant = false;
                isSliderCompletePlant = true;
                if (plantingInProgress && isSliderCompletePlant)
                {
                    // Ekim iþlemini burada gerçekleþtir
                    FarmFieldManager.instance.SetPlantColor();
                    Plant++;
                    Money--;
                    plantingInProgress = false; // Ekim iþlemi tamamlandý
                }
            }
        }
    }

    public void TimerHarvest()
    {
        if (timerActiveHarvest && !isSliderCompleteHarvest)
        {
            timerHarvest += Time.deltaTime;
            float normalizedTime = timerHarvest / timerMaxHarvest;
            HarvestSlider.value = normalizedTime;
            if (timerHarvest >= timerMaxHarvest)
            {
                timerHarvest = 0;
                timerActiveHarvest = false;
                isSliderCompleteHarvest = true;
                if (isSliderCompleteHarvest)
                {
                    // Hasat iþlemini burada gerçekleþtir
                    FarmFieldManager.instance.SetHarvestColor();
                    ReadyPlant++;
                    Plant--;
                }
            }
        }
    }

    public void PlantClicked()
    {
        if (Money > 0 && isSliderCompletePlant && !plantingInProgress)
        {
            timerActivePlant = true;
            isSliderCompletePlant = false;
            plantingInProgress = true; // Ekim iþlemi baþladý
        }
    }

    public void HarvestClicked()
    {
        if (Plant > 0 && isSliderCompleteHarvest)
        {
            timerActiveHarvest = true;
            isSliderCompleteHarvest = false;
        }
    }

    public void SellClicked()
    {
        if (ReadyPlant > 0)
        {
            FarmFieldManager.instance.SetSellColor();
            ReadyPlant--;
            SoldPlant++;
            Money = Money + 2;
        }
    }

    private void UpdateUI()
    {
        PlantedText.text = Plant.ToString() + " Planted";
        HarvestedText.text = ReadyPlant.ToString() + " Harvested";
        SoldText.text = SoldPlant.ToString() + " Sold";
        MoneyText.text = Money.ToString() + " MONEE";
    }
}
