using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public RectTransform radarPanel;
    public RectTransform blipContainer;
    public GameObject blipPrefab;

    [Header("Settings")]
    public float detectionRange = 50f;
    public float radarRadius = 100f;

    private readonly List<GameObject> activeBlips = new();

    void Update()
    {
        UpdateRadar();
    }

    void UpdateRadar()
    {
        // Remove previous frame's blips
        foreach (GameObject blip in activeBlips)
        {
            if (blip != null)
                Destroy(blip);
        }

        activeBlips.Clear();

        // Find all asteroids by tag
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject asteroid in asteroids)
        {
            Vector2 offset = asteroid.transform.position - player.position;
            float distance = offset.magnitude;

            // Skip objects outside radar range
            if (distance > detectionRange)
                continue;

            // Convert world position to radar position
            Vector2 normalized = offset / detectionRange;
            Vector2 radarPosition = normalized * radarRadius;

            // Create UI blip
            GameObject blip = Instantiate(blipPrefab, blipContainer);
            RectTransform rect = blip.GetComponent<RectTransform>();
            rect.anchoredPosition = radarPosition;

            activeBlips.Add(blip);
        }
    }
}