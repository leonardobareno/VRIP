using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Fuente: https://forums.oculusvr.com/developer/discussion/39327/gaze-pointers-drop-down-boxes
 */
public class CanvasVRModifier : MonoBehaviour
{
    public GameObject pointer;
    public GameObject canvas_go_inst;

    private bool bVRAdj_exec = false;

    private void Awake()
    {
        GraphicRaycaster gr = GetComponent<GraphicRaycaster>();

        OVRRaycaster OVRrc = null;

        bVRAdj_exec = false;

        /*Transform blocker_tr = canvas_go_inst.transform.Find("Blocker");
        OVRRaycaster blocker_ovrraycaster = null;*/

        if (gr != null)
        {
            gr.enabled = false;

            OVRrc = gameObject.AddComponent<OVRRaycaster>();
            OVRrc.pointer = pointer;
            OVRrc.blockingObjects = OVRRaycaster.BlockingObjects.All;
            //OVRrc.blockingObjects = OVRRaycaster.BlockingObjects.None;


            /*
            Debug.Log("BLOCKER=" + blocker_tr + "|");
            blocker_tr.GetComponent<GraphicRaycaster>().enabled = false;
            blocker_ovrraycaster = blocker_tr.gameObject.AddComponent<OVRRaycaster>();
            blocker_ovrraycaster.pointer = pointer;*/

        }
    }

    public GameObject[] listafondo;

    private void Update() {
        if (!bVRAdj_exec) {
            Transform blocker_tr = canvas_go_inst.transform.Find("Blocker");
            //Debug.Log("BLOCKER=" + blocker_tr + "|");
            if (blocker_tr != null) {
                Debug.Log("************* BLOCKER ");

                //SUC
                blocker_tr.GetComponent<GraphicRaycaster>().enabled = false;
                /*OVRRaycaster blocker_ovrraycaster = blocker_tr.gameObject.AddComponent<OVRRaycaster>();
                blocker_ovrraycaster.blockingObjects = OVRRaycaster.BlockingObjects.None;*/

                //blocker_ovrraycaster.pointer = OVRGazePointer.instance.gameObject;

                /*CanvasGroup cgro_inst = blocker_tr.gameObject.AddComponent<CanvasGroup>();
                cgro_inst.interactable = true;
                cgro_inst.blocksRaycasts = true;*/

                foreach(GameObject go_inst in listafondo) {
                    go_inst.SetActive(false);
                }
            }

            bVRAdj_exec = true;
        }

    }

    public void restore_gos() {
        foreach (GameObject go_inst in listafondo) {
            go_inst.SetActive(true);
        }
    }
}
