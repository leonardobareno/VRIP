using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class datetoggle_manager : MonoBehaviour {

    public GameObject sliderset_inst;
    private Toggle toggle_inst;

	// Use this for initialization
	void Start () {
        toggle_inst = gameObject.GetComponent<Toggle>();
    }

    public void toggleChangeEvent() {
        sliderset_inst.SetActive(toggle_inst.isOn);
    }
}
