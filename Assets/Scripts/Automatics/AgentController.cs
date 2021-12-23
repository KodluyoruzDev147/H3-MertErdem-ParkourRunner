using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Automatics
{
    [SelectionBase]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentController : MonoBehaviour
    {
        //components
        private NavMeshAgent agent;
        private Animator animator;
        private Rigidbody rigidBody;

        [SerializeField] private Transform finishLine;
        private Vector3 targetDestination;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();

            targetDestination = new Vector3(
                transform.position.x,
                transform.position.y,
                finishLine.position.z + 6);

            GameManager.ActionStart += Move;
        }

        private void Move()
        {
            agent.destination = targetDestination;
            animator.SetTrigger("Run");
        }

        private IEnumerator PushBack()
        {
            rigidBody.AddForce(-transform.forward * 10, ForceMode.Impulse);

            yield return new WaitForSeconds(1f);

            rigidBody.velocity = Vector3.zero;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                animator.SetTrigger("Dance");
            }               
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
                StartCoroutine(PushBack());
        }
    }
}
