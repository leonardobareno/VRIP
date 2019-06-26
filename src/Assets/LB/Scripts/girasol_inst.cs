using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


namespace LB_sw {
    public class girasol_inst : MonoBehaviour {

        public GameObject go_camara;
        public static bool bEsEsquemaPerspectivo = true;

        /*[Serializable]
        public class GirasolClickedEvent : UnityEvent<bool> { }

        [SerializeField]
        private GirasolClickedEvent onClick = new GirasolClickedEvent();*/

        // Use this for initialization
        /*void Start() {
        }*/

        // Update is called once per frame
        void Update() {
            //Debug.Log("############### PERSPECTIVO = " + bEsEsquemaPerspectivo);

            //esquema perspectivo
            if (bEsEsquemaPerspectivo) gameObject.transform.rotation = Quaternion.LookRotation((gameObject.transform.position - go_camara.transform.position).normalized, go_camara.transform.up);
            //esquema ortogonal
            else gameObject.transform.rotation = go_camara.transform.rotation;
        }

    }
}
