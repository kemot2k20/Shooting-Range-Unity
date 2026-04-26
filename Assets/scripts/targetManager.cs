using UnityEngine;
using System.Collections;
using TMPro;

public class TargetManager : MonoBehaviour
{
    private float minZ = 9f;
    private float maxZ = 17f;
    public GameObject targetHolder;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void AddPoint()
    {
        score++;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void SpawnTarget(float x, float y)
    {
        StartCoroutine(SpawnRoutine(x, y));
    }

    IEnumerator SpawnRoutine(float x, float y)
    {
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPos = new Vector3(x, y, randomZ);
        
        yield return new WaitForSeconds(2f);
        
        Instantiate(targetHolder, spawnPos, Quaternion.identity);
    }
}