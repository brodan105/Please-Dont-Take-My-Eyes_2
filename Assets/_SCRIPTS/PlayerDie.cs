using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    public static PlayerDie instance;

    [Header("References")]
    [SerializeField] ParticleSystem deathParticlePrefab;
    public Transform playerSpawn;
    public Animator fadePanel;
    [SerializeField] GameObject playerGFX;
    [SerializeField] GameObject runParticle;

    [Header("Properties")]
    [SerializeField] float respawnTime = 5f;
    [SerializeField] bool bossLevel = false;

    Rigidbody2D rb;

    Vector3 lastDeathLocation;

    CinemachineImpulseSource impulseSource;

    public bool hasDied = false;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (hasDied)
        {
            transform.position = lastDeathLocation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Death" && !hasDied)
        {
            Die();
        }
    }

    private void Respawn()
    {
        // Set timer to 0
        //TimeController.instance.ResetTimer();

        // Set playerFirstMove to true so it'll check the players movement to start timer
        //PlayerMovement.instance.playerFirstMove = true;

        // Move character to spawn
        transform.position = playerSpawn.position;

        // Reset player movement
        PlayerMovement p = GetComponent<PlayerMovement>();
        p.StartMovement();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Enable Run Particle
        runParticle.SetActive(true);

        // Start fade in
        StartCoroutine(fadeInDelay());

        // Enable player body
        playerGFX.GetComponent<SpriteRenderer>().enabled = true;

        hasDied = false;
    }

    private void RestartLevel()
    {
        SceneController.instance.ReloadBossScene();
    }

    public void Die()
    {
        hasDied = true;

        // Stop speedrun timer
        //TimeController.instance.StopTimer();

        // Update tally count
        TallyCountManager.instance.deathCount++;

        // Play SFX
        var sfx = PlayerAudioController.instance;
        sfx.playerDeath();

        // Disable player controls
        PlayerMovement p = GetComponent<PlayerMovement>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        p.StopMovement();

        lastDeathLocation = transform.position;

        // Shake camera
        CameraShakeManager.instance.CameraShake(impulseSource);

        // Spawn death particle
        Instantiate(deathParticlePrefab, transform.position, transform.rotation);

        if (!bossLevel)
        {
            // Increase vignette to 100 (use animator)
            fadePanel.SetTrigger("FadeOut");
        }

        // Disable player body
        playerGFX.GetComponent<SpriteRenderer>().enabled = false;

        // Disable Run Particle
        runParticle.SetActive(false);

        // Play respawn coroutine to count down to reset player
        StartCoroutine(respawnTimer());
    }

    public void SetPlayerRespawn(Transform respawn)
    {
        playerSpawn = respawn;
    }

    #region Timers
    private IEnumerator fadeInDelay()
    {
        yield return new WaitForSeconds(0.5f);

        // Decrease Vignette to default (animator)
        fadePanel.SetTrigger("FadeIn");

        // Reset check for input
        PlayerMovement.instance.CheckForInputDelay();
    }

    private IEnumerator respawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);

        if (!bossLevel)
        {
            // Respawn player
            Respawn();
        }
        else
        {
            RestartLevel();
        }
    }
    #endregion
}
