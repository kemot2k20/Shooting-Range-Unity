using UnityEngine;

public class StartButton : MonoBehaviour
{
    public ChallengeManager challengeManager;
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    public void Hit()
    {
        if (challengeManager != null)
        {
            challengeManager.ArmChallenge();
        }
        
        GetComponent<Renderer>().material.color = Color.red;
        Invoke("ResetColor", 0.5f);
    }

    void ResetColor()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
}