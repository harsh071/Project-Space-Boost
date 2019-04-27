﻿using UnityEngine.SceneManagement;

using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update


    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }

    private void ProcessInput()
    {
    }


    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {


        rigidbody.freezeRotation = true;


        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(-Vector3.forward * rotationThisFrame);

        }

        rigidbody.freezeRotation = false;

    }


    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) {
            return;
        }
        
        switch (collision.gameObject.tag)
        {
        
            case "Friendly":
                
                
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);                
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private  void LoadFirstLevel()
    {
       
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}