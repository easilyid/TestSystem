using UnityEngine;

namespace InventorySystem.Scripts
{
    public class PlayMove : MonoBehaviour
    {
        Animator animator;
        BoxCollider2D boxCollider2D;
        Rigidbody2D rb;

        [SerializeField] private float Speed = 0;
        private Vector2 vector2;

        public GameObject mybag;
        private void Start()
        {
            animator = GetComponent<Animator>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
            SwitchANI();
            OPenBag();
        }

        private void SwitchANI()
        {
            if (vector2!=Vector2.zero)
            {
                animator.SetFloat("horizontal",vector2.x);
                animator.SetFloat("vertical",vector2.y);
            }
            animator.SetFloat("speed" ,vector2.sqrMagnitude);
        }

        private void Move()
        {
            vector2.x = Input.GetAxisRaw("Horizontal");
            vector2.y = Input.GetAxisRaw("Vertical");
            rb.MovePosition(rb.position + vector2 * (Speed * Time.deltaTime));
        }

        void OPenBag()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                mybag.SetActive(!mybag.activeSelf);
            }
        }
    }
}