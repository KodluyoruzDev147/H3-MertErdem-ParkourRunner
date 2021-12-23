using UnityEngine;

namespace Game.Obstacles
{
    public class DynamicObstacle : MonoBehaviour
    {
        private Vector3 pointA, pointB, currentTarget;
        [SerializeField] [Range(0f, 5f)] private float speed = 5f;

        private void Start()
        {
            pointA = transform.position;
            pointB = new Vector3(-pointA.x, pointA.y, pointA.z);
            currentTarget = pointB;
        }

        private void FixedUpdate() => Patrol();

        private void Patrol()
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime);

            if (transform.position.x == currentTarget.x)
            {
                if (currentTarget == pointB)
                    currentTarget = pointA;
                else
                    currentTarget = pointB;
            }
        }
    }
}
