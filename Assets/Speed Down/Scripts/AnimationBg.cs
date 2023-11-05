using UnityEngine;

namespace Speed_Down.Scripts
{
    public class AnimationBg : MonoBehaviour
    {
        Material material;
        Vector2 movement;

        public Vector2 speed;

        void Start()
        {
            material = GetComponent<Renderer>().material;
        }

        void Update()
        {
            movement += speed * Time.deltaTime;
            material.mainTextureOffset = movement;
        }
    }
}