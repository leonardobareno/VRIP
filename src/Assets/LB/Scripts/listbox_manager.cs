using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listbox_manager : MonoBehaviour {

    public GameObject[] listafondo;

    public void Show() {
        gameObject.SetActive(true);

        foreach (GameObject go_inst in listafondo) {
            go_inst.SetActive(false);
        }
    }

    public void SaveHide() {
        //PENDIENTE Guardar

        foreach (GameObject go_inst in listafondo) {
            go_inst.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
