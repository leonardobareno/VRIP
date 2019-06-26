using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class okbutton_picdetails_manager : MonoBehaviour, IPointerClickHandler {

    public GameObject go_PicDetailsCanvas;
    public Camera camera_inst;
    public GameObject go_MenuButton;
    public GameObject go_MRPrjController;
    public GameObject go_origen_objs;
    public GameObject go_ejes_flechas;

    public void OnPointerClick(PointerEventData eventData) {

        Debug.Log("############### okbutton_picdetails_manager.OnPointerClick()");

        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
            Debug.Log("*********** OVRInput.Button.PrimaryTouchpad");
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            Debug.Log("*********** PrimaryIndexTrigger");
        if (OVRInput.Get(OVRInput.Button.One))
            Debug.Log("*********** ONE UP");
        if (OVRInput.Get(OVRInput.Button.Two))
            Debug.Log("*********** TWO UP");

        //camera_inst.cullingMask = 1023; //all layers + Default + PicLayer + Gazable
        go_origen_objs.SetActive(true);
        go_ejes_flechas.SetActive(true);
        go_MenuButton.SetActive(true);
        go_PicDetailsCanvas.SetActive(false);
        go_MRPrjController.GetComponent<app_controller>().Sonido("salidasettings");
        go_MRPrjController.GetComponent<picdetails_manager>().restaurarPictureDims();
    }
}
