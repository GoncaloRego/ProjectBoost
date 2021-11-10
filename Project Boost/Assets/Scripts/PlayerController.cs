using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] AudioClip turbineSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem turbineParticles;
    Rigidbody rocketRigidbody;
    Transform cachedTransform;
    AudioSource audioSource;

    bool gameIsTransitioning = false;

    void Start()
    {
        cachedTransform = transform;
        rocketRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MovementControl();
    }

    void MovementControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(turbineSound);
            }
            rocketRigidbody.AddRelativeForce(Vector3.up * jumpSpeed * Time.deltaTime);
            if (turbineParticles.isPlaying == false)
            {
                turbineParticles.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying == true)
            {
                audioSource.Stop();
            }
            if (turbineParticles.isPlaying == true)
            {
                turbineParticles.Stop();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rocketRigidbody.freezeRotation = true;
            cachedTransform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
            rocketRigidbody.freezeRotation = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rocketRigidbody.freezeRotation = true;
            cachedTransform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
            rocketRigidbody.freezeRotation = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (gameIsTransitioning)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Obstacle":
                gameIsTransitioning = true;
                audioSource.Stop();
                audioSource.PlayOneShot(deathSound);
                GetComponent<PlayerController>().enabled = false;
                crashParticles.Play();
                Invoke(nameof(ReloadLevel), 1f);
                break;
            case "Finish":
                gameIsTransitioning = true;
                audioSource.Stop();
                audioSource.PlayOneShot(successSound);
                GetComponent<PlayerController>().enabled = false;
                successParticles.Play();
                //Invoke(nameof(LoadNextLevel), 1f);
                break;
            default:
                break;
        }
    }

    void ReloadLevel()
    {
        gameManager.ReloadLevel();
    }

    void LoadNextLevel()
    {
        gameManager.LoadNextLevel();
    }
}
