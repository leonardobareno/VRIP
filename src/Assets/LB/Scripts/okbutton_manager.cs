using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SQLite4Unity3d;
using LB_sw;

public class okbutton_manager : MonoBehaviour {

    public GameObject go_MenuButton;
    public GameObject go_OpcionesCanvas;
    public GameObject go_origen_objs;
    public GameObject go_ejes_flechas;

    public Camera camera_inst;

    public GameObject go_MRPrjController;

    public void clickEvent() {
        go_OpcionesCanvas.SetActive(false);
        //camera_inst.cullingMask = 1023; //all layers + Default + PicLayer + Gazable
        go_origen_objs.SetActive(true);
        go_ejes_flechas.SetActive(true);
        go_MenuButton.SetActive(true);

        go_MRPrjController.GetComponent<app_controller>().Sonido("salidasettings");
        go_MRPrjController.GetComponent<settings_manager>().StartGenerarGirasoles();
    }
}
