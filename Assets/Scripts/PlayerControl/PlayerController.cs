using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerControl
{
    [SelectionBase]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerBase;
        [SerializeField] private Transform finishLine;
        private Vector3 targetPos;
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
        private DanceState danceState;


        private void Awake()
        {
            GameManager.ActionStart += StartRunning;

            rigidBody = GetComponent<Rigidbody>();
            animator = playerBase.GetComponent<Animator>();

            targetPos = new Vector3(
                transform.position.x,
                transform.position.y,
                finishLine.position.z + 6);

            idleState = new IdleState(this);
            runState = new RunState(this);
            danceState = new DanceState(this);
            SetState(idleState);
        }

        void Update()
        {
            /* ZTK was here
             * Bu tarz oyun kontrolleri Joystick mantığında değil daha çok slide kontrolleri kullanılır.
             * Slide derken parmağın yaptığı hareket kadar oyun karakteri hareket edecek şekilde.
             */
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
                        targetPos,
                        speed * Time.fixedDeltaTime);

            var playerBasePos = playerBase.transform.localPosition;
            playerBasePos.x = Mathf.Clamp(playerBasePos.x + horizontal * horizontalSpeed * Time.fixedDeltaTime, -2f, 2f);
            playerBase.transform.localPosition = playerBasePos;
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
            {
                StartCoroutine(PushBack());
            }
                
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                SetState(danceState);
                GameManager.ActionFinish?.Invoke();
            }
                
        }

        private void OnDestroy()
        {
            GameManager.ActionStart -= StartRunning;
        }
    }
}
