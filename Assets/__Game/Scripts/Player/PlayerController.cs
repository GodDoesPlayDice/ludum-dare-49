using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // movement variables
    public float movementSpeed = 4f;
    private Rigidbody rb;
    private Vector3 movement;

    // animation variables
    private Animator animator;

    // platform
    private Transform platformTransform;

    // audio
    private AudioSource audioSource;

    [SerializeField]
    private float distanceToActivate = 1.1f;

    public Action action { private get; set; }

    public List<UpDownActionSource> actions;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        platformTransform = GameObject.FindGameObjectWithTag("Platform").transform;
        audioSource = GetComponent<AudioSource>();

        actions = FindByType<UpDownActionSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // input for movement
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        movement = new Vector3(xAxis, 0, zAxis);

        // input for action
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ExecuteActionsIfAvailable(true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ExecuteActionsIfAvailable(false);
        }


        // animation
        if (animator != null)
        {
            if (movement.magnitude >= 0.1f)
            {
                animator.SetFloat("speed", movement.magnitude);
            } else
            {
                animator.SetFloat("speed", 0);
            }
        }

        // sound
        if (new Vector2(rb.velocity.x, rb.velocity.z).magnitude >= 2f)
        {
            //play footsteps sound
            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1f);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            //stop footsteps sound
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();


        // is close to worker
        foreach (UpDownActionSource source in actions)
        {
            if (Vector3.Distance(source.transform.position, transform.position) < distanceToActivate)
            {
                source.IsCloseToPlayer(true);
            }
            else
            {
                source.IsCloseToPlayer(false);
            }
        }
    }

    private void Movement()
    {
        if (rb == null) return;
        Vector3 offset = movement * movementSpeed;

        // no movement if platform is too inclined
        if (platformTransform != null)
        {
            if (Vector3.Angle(platformTransform.up, Vector3.up) > 30)
            {
                offset = rb.velocity;
            }
        }

        // movement
        if (movement.magnitude >= 0.01f)
        {
            rb.velocity = new Vector3(offset.x, rb.velocity.y, offset.z);
        }
    }

    private void Rotation()
    {
        if  (movement.magnitude >= 0.1f)
        {
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement, Vector3.up), 0.25f));
        }
    }

    public void ExecuteActionsIfAvailable(bool down)
    {
        
        foreach (UpDownActionSource source in actions)
        {
            if (Vector3.Distance(source.transform.position, transform.position) < distanceToActivate)
            {
                source.ExecuteAction(down ? PlayerActionType.DOWN : PlayerActionType.UP);
            }
        }
    }

    public static List<T> FindByType<T>()
    {
        List<T> interfaces = new List<T>();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            T[] childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
            foreach (var childInterface in childrenInterfaces)
            {
                interfaces.Add(childInterface);
            }
        }
        return interfaces;
    }
}
