using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Project
{
    public class CostMultiplierEffect : PooledBehaviour
    {
        [SerializeField, Space]
        private TextMeshPro _text = null;
        [SerializeField]
        private DOTweenAnimation _onEnableTween = null;
        
        protected override void BeforeReturnToPool()
        {
            base.BeforeReturnToPool();
            
            _onEnableTween.gameObject.SetActive(false);
        }

        public void Setup(float costMultiplier)
        {
            _onEnableTween.gameObject.SetActive(true);
            _text.text = ($"X{costMultiplier}").Replace(',','.');
        }
    }
}