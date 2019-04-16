using UnityEngine.SceneManagement;

using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update


    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Thrust();
        Rotate();

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
        
        switch (collision.gameObject.tag)
        {
        
            case "Friendly":
                
                
                break;
            case "Finish":
           
                    SceneManager.LoadScene(1);
                
                break;
            default:
                SceneManager.LoadScene(0);
                break;
        }
    }
}