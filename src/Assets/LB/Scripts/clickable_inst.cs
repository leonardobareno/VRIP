using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickable_inst : MonoBehaviour, IPointerClickHandler {

    //PK de wga_hu
    public string filename;

    public GameObject go_MRPrjController;

    public void OnPointerClick(PointerEventData eventData) {
        //Debug.Log("clickable_inst.OnPointerClick()=" + filename + "|" + eventData + "|");
        //yield return null;

        //Debug.Log("girasol_manager.girasolInst_ClickEvent()=");

        //Debug.Log("girasol_manager.girasolInst_ClickEvent() pointerId=" + eventData.button + "|");

        //Debug.Log("############ app_controller nButton=" + go_MRPrjController.GetComponent<app_controller>().nButton);



        /*if (OVRInput.GetUp(OVRInput.Button.One))
            Debug.Log("*********** ONE UP");
        if (OVRInput.GetUp(OVRInput.Button.Two))
            Debug.Log("*********** TWO UP");
        if (OVRInput.GetUp(OVRInput.Button.Three))
            Debug.Log("*********** THREE UP");
        if (OVRInput.GetUp(OVRInput.Button.Four))
            Debug.Log("*********** FOUR UP");*/

        Debug.Log("################## clickable_inst.OnPointerClick()");
        
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
            Debug.Log("*********** OVRInput.Button.PrimaryTouchpad");
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            Debug.Log("*********** PrimaryIndexTrigger");
        if (OVRInput.Get(OVRInput.Button.One))
            Debug.Log("*********** ONE UP");
        if (OVRInput.Get(OVRInput.Button.Two))
            Debug.Log("*********** TWO UP");

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButton(0)) {
            go_MRPrjController.GetComponent<app_controller>().Sonido("detalles");
            go_MRPrjController.GetComponent<picdetails_manager>().LoadShowPicDetails(filename);
        }
    }

}
