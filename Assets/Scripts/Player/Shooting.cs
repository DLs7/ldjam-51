using UnityEngine;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        public Transform Firepoint;
        public GameObject Fireball;

        public float FireballForce = 20f;
    
        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            GameObject fireball = Instantiate(Fireball, Firepoint.position, Firepoint.rotation);
            Rigidbody2D rigidbody2D = fireball.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Firepoint.up * FireballForce, ForceMode2D.Impulse);
        }
    }
}
