// #if UNITY_EDITOR
// using System.Collections;
// using System.Collections.Generic;
// using EasyButtons;
// using Project;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class UITuner : MonoBehaviour
// {
//    [SerializeField]
//    private Material _materialFast = null;
//
//    [Button]
//    public void TuneUI()
//    {
//       GetComponentsInChildren<Image>().Do(x=>
//       {
//          x.material = _materialFast;
//
//          x.maskable = false;
//          
//          x.raycastTarget = x.TryGetComponent(out Button button);
//          
//       });
//    }
// }
// #endif
