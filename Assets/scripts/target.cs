using UnityEngine;

public class target : MonoBehaviour
{

    public ChallengeManager challengeManager;
    public void Die()
    {
        float oldX = transform.position.x;
        float oldY = transform.position.y;

        TargetManager manager = FindFirstObjectByType<TargetManager>();

        if (manager != null)
        {
            manager.SpawnTarget(oldX, oldY);
            manager.AddPoint();
        }

        if (challengeManager != null)
        {
            challengeManager.AddKill();
        }

        Destroy(gameObject);
    }
}