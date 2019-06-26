using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class app_controller : MonoBehaviour {

    public GameObject punto_origen;
    public Transform camara_transf_inst;

    public OVRInputModule m_EyeRaycaster;

    private float fSpeedFactor = 1.0f;
    private float fSpeedFactorTranslation = 2.5f;
    private int nEstado;
    private Vector2 v2_trackpad_delta;
    private Vector2 v2_trackpad_abs;
    private float fTrackpad_pos_y_ant;

    private float fDeltaY;

//    private float fDistanciaTrackpad;
    private bool bMoviendo;
    private bool bMoviendoAbs;
    private bool bOneButtonPressed;

    public AudioSource audio_inst;

    public AudioClip audioSettings;
    public AudioClip audioTraslacion;
    public AudioClip audioDetalles;
    public AudioClip audioSalidasettings;
    public AudioClip audioStart;

    private bool bSonido = false;

    public void Sonido(string sAudio) {
        if (bSonido) {
            AudioClip sel = audioDetalles;
            if (sAudio == "settings") sel = audioSettings;
            else if (sAudio == "traslacion") sel = audioTraslacion;
            else if (sAudio == "salidasettings") sel = audioSalidasettings;
            else if (sAudio == "inicio") sel = audioStart;

            audio_inst.PlayOneShot(sel);
        }
    }

    // Use this for initialization
    void Start () {
        nEstado = 0;
        bMoviendo = false;
        bOneButtonPressed = false;
        Sonido("inicio");
	}

    //public int nButton = -1;
	
	// Update is called once per frame
	void Update () {
        //Distancia absoluta
        v2_trackpad_abs.Set(Input.GetAxis("Oculus_GearVR_DpadX"), Input.GetAxis("Oculus_GearVR_DpadY"));
        //Delta
        v2_trackpad_delta.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


        /*Debug.Log("MOUSE " 
            + Input.GetAxis("Mouse X") + " | " 
            + Input.GetAxis("Mouse Y")
            );*/

        /*Debug.Log("MOUSE ABS " 
            + v2_trackpad_abs.x + " | " 
            + v2_trackpad_abs.y
            );*/


        //SUC
        /*Debug.Log("DPAD "
            + Input.GetAxis("Oculus_GearVR_DpadX") + " | "
            + Input.GetAxis("Oculus_GearVR_DpadY"));*/

        /*if (OVRInput.GetDown(OVRInput.Button.DpadUp))
            Debug.Log("DPADUP");
        if (OVRInput.GetDown(OVRInput.Button.DpadDown))
            Debug.Log("DPADDOWN");
        if (OVRInput.GetDown(OVRInput.Button.DpadLeft))
            Debug.Log("DPADLEFT");
        if (OVRInput.GetDown(OVRInput.Button.DpadRight))
            Debug.Log("DPADRIGHT");

        if (OVRInput.GetDown(OVRInput.Button.Up))
            Debug.Log("UP");
        if (OVRInput.GetDown(OVRInput.Button.Down))
            Debug.Log("DOWN");
        if (OVRInput.GetDown(OVRInput.Button.Left))
            Debug.Log("LEFT");
        if (OVRInput.GetDown(OVRInput.Button.Right))
            Debug.Log("RIGHT");

        if (OVRInput.GetDown(OVRInput.Button.One))
            Debug.Log("ONE");
        if (OVRInput.GetDown(OVRInput.Button.Two))
            Debug.Log("TWO");
        if (OVRInput.GetDown(OVRInput.Button.Three))
            Debug.Log("THREE");
        if (OVRInput.GetDown(OVRInput.Button.Four))
            Debug.Log("FOUR");*/

        /*if (OVRInput.GetDown(OVRInput.Button.One)) {
            Debug.Log("########### ONE UP");
            nButton = 1;
        }
        else if (OVRInput.GetDown(OVRInput.Button.Two)) {
            Debug.Log("TWO UP");
            nButton = 2;
        }
        else {
            nButton = -1;
        }*/

        /*else if (OVRInput.GetUp(OVRInput.Button.Three))
            Debug.Log("THREE UP");
        else if (OVRInput.GetUp(OVRInput.Button.Four))
            Debug.Log("FOUR UP");*/

        if (OVRInput.GetDown(OVRInput.Button.One)) {
            bOneButtonPressed = true;
        }

        if (OVRInput.GetUp(OVRInput.Button.One)) {
            bOneButtonPressed = false;
        }

        //camara_transf_inst.RotateAround(punto_origen.transform.position, camara_transf_inst.transform.up, SpaceNavigator.Rotation.Roll() * Mathf.Rad2Deg * fSpeedFactor);

        fDeltaY = v2_trackpad_abs.y - fTrackpad_pos_y_ant;
        fTrackpad_pos_y_ant = v2_trackpad_abs.y;

        bMoviendo = !bOneButtonPressed && (Mathf.Abs(v2_trackpad_delta.x) > 1e-04 || Mathf.Abs(v2_trackpad_delta.y) > 1e-04);
        bMoviendoAbs = bOneButtonPressed && Mathf.Abs(fDeltaY) > 1e-04;

        /*Debug.Log("MOUSE fDeltaY = " + fDeltaY);*/

        if (nEstado == 0)
        {
            //ignorar la primera lectura
            if (bMoviendo || bMoviendoAbs) {
                //v2_ini_pos.Set(v2_trackpad_pos.x, v2_trackpad_pos.y);

                nEstado = 1;
                if (m_EyeRaycaster != null) {
                    m_EyeRaycaster.ShowLineRenderer = false;
                }
                if (bMoviendoAbs) {
                    Sonido("traslacion");
                }
            }
        }
        else if (nEstado == 1) {

            if (bMoviendoAbs) {
                camara_transf_inst.Translate(camara_transf_inst.transform.forward * fDeltaY * fSpeedFactorTranslation, Space.World);
            }
            else if (bMoviendo) {
                camara_transf_inst.RotateAround(punto_origen.transform.position, Vector3.up, v2_trackpad_delta.x * fSpeedFactor * -1.0f);
                camara_transf_inst.RotateAround(punto_origen.transform.position, camara_transf_inst.transform.right, v2_trackpad_delta.y * fSpeedFactor * -1.0f);
                //camara_transf_inst.RotateAround(punto_origen.transform.position, camara_transf_inst.transform.up, SpaceNavigator.Rotation.Roll() * Mathf.Rad2Deg * fSpeedFactor);
            }
            else {
                nEstado = 0;
                if (m_EyeRaycaster != null) {
                    m_EyeRaycaster.ShowLineRenderer = true;
                }
            }
        }

    }
}
