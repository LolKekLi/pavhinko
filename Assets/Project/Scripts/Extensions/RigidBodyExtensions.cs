using UnityEngine;

namespace Project
{
    public static class RigidBodyExtensions
    {
        public static void Enable(this Rigidbody rigidbody)
        {
            rigidbody.isKinematic = false;
            rigidbody.constraints = ~RigidbodyConstraints.FreezeAll;
            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = true;
        }
        
        public static void Disable(this Rigidbody rigidbody)
        {
            rigidbody.isKinematic = true;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = false;
        }

        public static void ResetForce(this Rigidbody rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}