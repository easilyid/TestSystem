using UnityEngine;

namespace Speed_Down.Scripts
{
    public class FanPlatform : MonoBehaviour
    {
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                animator.Play("Fan_run");
            }
        }
    }
}
