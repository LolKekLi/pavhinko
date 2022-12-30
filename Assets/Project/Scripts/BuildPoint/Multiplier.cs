using UnityEngine;

namespace Project
{
    public class Multiplier : BuildingBase
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball))
            {
                
            }
        }
    }
}