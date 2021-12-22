using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerControl
{
    [SelectionBase]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerBase;
        [SerializeField] private Transform targetPos;
        //inputs
        private float horizontal = 0;
        [SerializeField] private Joystick joystick;
        //specs
        [Header("Speed Settings")]
        [SerializeField] [Range(0f, 10f)] private float speed = 10f;
        [SerializeField] [Range(0f, 10f)] private float horizontalSpeed = 5f;
        //components
        private Rigidbody rigidBody;
        [System.NonSerialized] public Animator animator;
        //states
        private State currentState;
        private IdleState idleState;
        private RunState runState;


        private void Awake()
        {
            GameManager.ActionStart += StartRunning;

            rigidBody = GetComponent<Rigidbody>();
            animator = playerBase.GetComponent<Animator>();

            idleState = new IdleState(this);
            runState = new RunState(this);
            SetState(idleState);
        }

        void Update()
        {
            horizontal = joystick.Horizontal;
        }

        private void FixedUpdate()
        {
            currentState.OnUpdate();
        }

        private void SetState(State newState)
        {
            if (currentState != null)
                currentState.OnStateExit();

            currentState = newState;
            currentState.OnStateEnter();
        }

        public void Move()
        {
            transform.position = Vector3.MoveTowards(
                        transform.position,
                        targetPos.position,
                        speed * Time.fixedDeltaTime);

            var playerBasePos = playerBase.transform.position;
            playerBasePos.x = Mathf.Clamp(playerBasePos.x + horizontal * horizontalSpeed * Time.fixedDeltaTime, -2f, 2f);
            playerBase.transform.position = playerBasePos;
        }

        //action method
        private void StartRunning()
        {
            SetState(runState);
        }

        private IEnumerator PushBack()
        {
            SetState(idleState);
            rigidBody.AddForce(-transform.forward * 10, ForceMode.Impulse);

            yield return new WaitForSeconds(1f);

            SetState(runState);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
                StartCoroutine(PushBack());
        }

        private void OnDestroy()
        {
            GameManager.ActionStart -= StartRunning;
        }
    }
}
