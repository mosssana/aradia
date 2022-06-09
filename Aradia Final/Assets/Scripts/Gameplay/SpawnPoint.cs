using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    /// <summary>
    /// Otherwise Difficulty Based
    /// </summary>
    [SerializeField] bool luckBased;
    [Range(0, 400)][SerializeField] int threshold;
    [SerializeField] GameObject objectToSpawnBeforeThreshold;
    [SerializeField] GameObject objectToSpawnAfterThreshold;

    void Awake()
    {
        if (!luckBased)
        {
            if (TarotCardsHand.CardsDifficultyScore >= threshold) Instantiate(objectToSpawnAfterThreshold, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            else if (objectToSpawnBeforeThreshold != null) Instantiate(objectToSpawnBeforeThreshold, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        else
        {
            if (TarotCardsHand.CardsTotalLuckScore >= threshold) Instantiate(objectToSpawnAfterThreshold, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            else if (objectToSpawnBeforeThreshold != null) Instantiate(objectToSpawnBeforeThreshold, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
