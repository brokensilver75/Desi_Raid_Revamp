using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class BloodSplotchesSystem : MonoBehaviour
{
    [Header("Reference setup")]
    [SerializeField] private ParticleSystem Particle;
    [SerializeField] private GameObject BloodDecal;

    [Header("Blood Decal Variation")]
    [SerializeField] private float GroundBloodOffsetY;
    [SerializeField] private Vector2 ScaleRandomizer;
    private readonly List<ParticleCollisionEvent> collisionEvent=new();

    private void Awake()
    {
        if (Particle == null)
        { 
            Particle = GetComponent<ParticleSystem>();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Vector3 spawnPos;
        int eventNumber = Particle.GetCollisionEvents(other, collisionEvent);
        for (int i = 0; i < eventNumber; i++)
        {
            ParticleCollisionEvent collision = collisionEvent[i];
            if (other.tag == "Untagged")
            {
                spawnPos = new Vector3(collision.intersection.x, GroundBloodOffsetY, collision.intersection.z);

                GameObject blood = Instantiate(BloodDecal, spawnPos, Quaternion.Euler(90f, Random.Range(0, 360), 0f));
                float Randomscale = Random.Range(ScaleRandomizer.x, ScaleRandomizer.y);
                blood.transform.localScale = new Vector3(3f * Randomscale, 3f * Randomscale, 1f);
            }
            else
            {
                float angleY=(Mathf.Atan2(collision.normal.x,collision.normal.z)*Mathf.Rad2Deg )+180;
                spawnPos = collision.intersection ;

                Quaternion rotation = Quaternion.Euler(0f, angleY, 0f);

                GameObject blood = Instantiate(BloodDecal, spawnPos, rotation);
                float Randomscale = Random.Range(ScaleRandomizer.x, ScaleRandomizer.y);
                blood.transform.localScale = new Vector3(3f * Randomscale, 3f * Randomscale, 1f);
            }
        }
    }
}
