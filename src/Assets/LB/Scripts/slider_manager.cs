using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using VRStandardAssets.Utils;
using UnityEngine.UI;


public class slider_manager : MonoBehaviour {

    /* [SerializeField] private VRInteractiveItem m_InteractiveItem;       // Reference to the VRInteractiveItem to determine when to fill the bar.
     [SerializeField] private VRInput m_VRInput;                         // Reference to the VRInput to detect button presses.
     [SerializeField] private Slider m_Slider;                           // Optional reference to the UI slider (unnecessary if using a standard Renderer).
     [SerializeField] private Collider m_Collider;                       // Optional reference to the Collider used to detect the user's gaze, turned off when the UIFader is not visible.

     private bool m_GazeOver;                                            // Whether the user is currently looking at the bar.

     private void OnEnable() {
         m_VRInput.OnDown += HandleDown;
         m_VRInput.OnUp += HandleUp;

         m_InteractiveItem.OnOver += HandleOver;
         m_InteractiveItem.OnOut += HandleOut;
     }


     private void OnDisable() {
         m_VRInput.OnDown -= HandleDown;
         m_VRInput.OnUp -= HandleUp;

         m_InteractiveItem.OnOver -= HandleOver;
         m_InteractiveItem.OnOut -= HandleOut;
     }

     private void HandleDown() {
         // If the user is looking at the bar start the FillBar coroutine and store a reference to it.
         //if (m_GazeOver)
         //    m_FillBarRoutine = StartCoroutine(FillBar());

         Debug.Log("****** HANDLE DOWN");
     }


     private void HandleUp() {
         // If the coroutine has been started (and thus we have a reference to it) stop it.
         //if (m_FillBarRoutine != null)
         //    StopCoroutine(m_FillBarRoutine);

         // Reset the timer and bar values.
         m_Timer = 0f;
         SetSliderValue(0f);

         Debug.Log("****** HANDLE UP");
     }


     private void HandleOver()  {
         // The user is now looking at the bar.
         m_GazeOver = true;
         Debug.Log("****** HANDLE OVER");
     }


     private void HandleOut() {
         // The user is no longer looking at the bar.
         m_GazeOver = false;
         Debug.Log("****** HANDLE OUT");
     }*/

    public Text caption;

    public void cambiar_mostrar_valor() { 
        caption.text = (int) gameObject.GetComponent<Slider>().value + " %";
    }

}
