using UnityEngine;
using UnityEngine.UI;

public class TimerObject : MonoBehaviour
{
    [SerializeField] private float targetTime= 1;
    [SerializeField] private float timer = 1;
    [SerializeField] private bool AutoHarvester = false;
    [SerializeField] private bool AutoPlanter = false;

    [SerializeField] private Image plantImage;
    [SerializeField] private Image harvestImage;
    bool startTimer = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (startTimer)
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                if (AutoHarvester)
                {
                    UIManager.Instance.HarvestClicked();
                }

                if (AutoPlanter)
                {
                    UIManager.Instance.PlantClicked();
                }

                targetTime = timer;
                startTimer = false;
                startTimer = true;   
            }
        }

        if (AutoHarvester)
        {
            AutoPlanter = false;
        }

        if (AutoPlanter)
        {
            AutoHarvester = false;
        }

        if (AutoPlanter)
        {
            if (targetTime < 5 && targetTime > 4)
            {
                plantImage.color = Color.green;
            }
            else if (targetTime < 4)
            {
                plantImage.color = Color.yellow;
            }
        }

        if (AutoHarvester)
        {
            if (targetTime < 5 && targetTime > 4)
            {
                harvestImage.color = Color.green;
            }
            else if (targetTime < 4)
            {
               harvestImage.color = Color.yellow;
            }
        }



    }

    public void StartTimer()
    {
        startTimer = true;
        timer = targetTime;
    }

    
}
