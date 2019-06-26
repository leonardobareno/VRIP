using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menubutton_manager : MonoBehaviour {

    public GameObject go_OpcionesCanvas;
    public GameObject go_PosRef_Opciones;
    public GameObject go_origen_objs;
    public GameObject go_ejes_flechas;

    public GameObject go_MRPrjController;

    public Camera camera_inst;

    public void mostrarOpciones() {
        //go_OpcionesCanvas.transform.localPosition = go_PosRef_Opciones.transform.localToWorldMatrix * go_PosRef_Opciones.transform.localPosition;
        //go_OpcionesCanvas.transform.localRotation = go_PosRef_Opciones.transform.localRotation;

        gameObject.SetActive(false);
        //camera_inst.cullingMask = 255;//todos excepto PicLayer y Gazable
        go_origen_objs.SetActive(false);
        go_ejes_flechas.SetActive(false);
        go_OpcionesCanvas.SetActive(true);

        go_MRPrjController.GetComponent<app_controller>().Sonido("settings");
    }
}
