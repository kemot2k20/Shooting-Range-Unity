using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Globalization;

public class ChallengeManager : MonoBehaviour
{
    public int targetKillsRequired = 50;
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI infoText;
    public TMP_Text resultText; 

    public AudioSource audioSource;
    public AudioClip finishSound;

    private bool isArmed = false;
    private bool isRunning = false;
    private float currentTime = 0f;
    private int currentKills = 0;

    public TargetManager targetManager;

    void Start()
    {
        ResetChallenge();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateUI();
        }
    }

    public void ArmChallenge()
    {
        if (isRunning) return;

        isArmed = true;
        currentKills = 0;
        currentTime = 0f;
        
        targetManager.scoreText.gameObject.SetActive(false);

        pointsText.gameObject.SetActive(true);
        infoText.gameObject.SetActive(true);
    }

    public void StartTimer()
    {
        if (isArmed && !isRunning)
        {
            isArmed = false;
            isRunning = true;
            timerText.gameObject.SetActive(true);
            infoText.gameObject.SetActive(false);
        }
    }
    public void AddKill()
    {
        if (!isRunning) return;

        currentKills++;
        pointsText.text = currentKills.ToString() + "/" + targetKillsRequired.ToString();

        if (currentKills >= targetKillsRequired)
        {
            EndChallenge();
        }
    }

    void EndChallenge()
    {
        audioSource.PlayOneShot(finishSound);
        isRunning = false;

        if (resultText != null) resultText.text = "TIME : " + currentTime.ToString("F2").Replace(',', '.') + "s";
        
        resultText.gameObject.SetActive(true);

        targetManager.scoreText.gameObject.SetActive(true);

        timerText.gameObject.SetActive(false);
        pointsText.gameObject.SetActive(false);
    }

    void ResetChallenge()
    {
        isArmed = false;
        isRunning = false;
        currentTime = 0f;
        currentKills = 0;
        
        if (timerText != null) {
            timerText.text = "00:00";
            timerText.gameObject.SetActive(false);
        }
        if (pointsText != null) {
            pointsText.text = "0" + "/" + targetKillsRequired.ToString();
            pointsText.gameObject.SetActive(false);
        }
        if (resultText != null)
        {
            resultText.text = "RESULT TEXT";
            resultText.gameObject.SetActive(false);
        }
        if (infoText != null)
        {
            infoText.text = "Shoot to start!";
            infoText.gameObject.SetActive(false);
        }
    }

    void UpdateUI()
    {
        if (timerText != null)
        {
            string minutes = Mathf.Floor(currentTime / 60).ToString("00");
            string seconds = (currentTime % 60).ToString("00.00", CultureInfo.InvariantCulture);
            timerText.text = minutes + ":" + seconds;
        }
    }
}