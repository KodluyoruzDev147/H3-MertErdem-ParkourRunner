using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playerBase;
    [SerializeField] private Transform finishLane;
    private Vector3 targetPos;
    //specs
    [SerializeField] [Range(0f, 10f)] private float speed = 10f, horizontalSpeed = 5f;
    //inputs
    private float horizontal = 0;
    //components
    private Animator animator;

    private bool isRunning = false;


    private void Awake()
    {
        GameManager.ActionStart += Run;

        targetPos = new Vector3(
            transform.position.x,
            transform.position.y,
            finishLane.position.z);

        animator = playerBase.GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!isRunning) return;
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    speed * Time.fixedDeltaTime);

        var playerBasePos = playerBase.transform.position;
        playerBasePos.x = Mathf.Clamp(playerBasePos.x + horizontal * horizontalSpeed * Time.fixedDeltaTime, -2f, 2f);
        playerBase.transform.position = playerBasePos;
    }

    private void Run()
    {
        isRunning = true;
        animator.SetTrigger("Run");
    }

    private void OnDestroy()
    {
        GameManager.ActionStart -= Run;
    }
}
