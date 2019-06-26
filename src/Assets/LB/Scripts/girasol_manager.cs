using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.EventSystems;

using LB_sw;
using SQLite4Unity3d;

//existe desde v 2018.3
//#if UNITY_ANDROID
//using UnityEngine.Android;
//#endif


public class girasol_manager : MonoBehaviour {

    Texture2D[] textList;

    //public string[] files;


    public GameObject ori_quad;
    public GameObject parent_origen;
    public GameObject go_ejes_flechas;

    public GameObject ori_esfera;
    public Material ori_esfera_material;

    //private float fEspacioLen = 10.0f;

    private int nDivisorReducTextura = 8;

    // Use this for initialization
    void Start()
    {
        //existe desde v 2018.3
        /*
        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)){
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
        #endif
        */

        //files = System.IO.Directory.GetFiles(path +  "wga_hu_1/g_gogh_van_10_portra08.jpg");

        /*
         Prueba velocidad de procesamiento
SDCARD
219 imgs en 11.28 seg, 19.41 img/seg

PHONE
239 imgs en 11.77 seg, 20.31 imgs/seg, 4.63% más rápido

         */

        //SDCARD
        //files = System.IO.Directory.GetFiles(path + "wga_hu_1/", "g_gogh_van_1*.jpg");//219 imgs
        //files = System.IO.Directory.GetFiles(path + "wga_hu_1/", "a_a*.jpg");//1456 imgs

        //PHONE
        //files = System.IO.Directory.GetFiles("/storage/emulated/0/wga_hu_detail/", "a_al*.jpg");//239 imgs

        //StartCoroutine(LoadImagesThread());
    }


    /*void Update()
    {

    }*/

    /*private IEnumerator LoadImagesThread() {
        LoadImages();
        yield return null;
    }*/

    private bool bDebug = false;
    System.Random rnd;

    public IEnumerator LoadImages(TableQuery<wga_hu> results, settings_manager sm_inst) {
        if (bDebug) Debug.Log("******* girasol_manager.LoadImages() inicio");

        double dSampleDistr = -1.0d;
        if (sm_inst.SamplePercSlider.value < 100.0f) {
            dSampleDistr = ((double)sm_inst.SamplePercSlider.value) / 100.0d;
            rnd = new System.Random(getTime());
        }

        float fScaleFactor = sm_inst.ItemScaleSlider.value / 100f;

        bool bEsfera = false;
        bEsfera = sm_inst.ItemShapeEsferaToggle.isOn;

        string sXCleanCaption = cleanCaption(sm_inst.XFeatureDropdown.options[sm_inst.XFeatureDropdown.value].text);
        string sYCleanCaption = cleanCaption(sm_inst.YFeatureDropdown.options[sm_inst.YFeatureDropdown.value].text);
        string sZCleanCaption = cleanCaption(sm_inst.ZFeatureDropdown.options[sm_inst.ZFeatureDropdown.value].text);

        if (bDebug) {
            Debug.Log("**** sXCleanCaption=" + sXCleanCaption + "|");
            Debug.Log("**** sYCleanCaption=" + sYCleanCaption + "|");
            Debug.Log("**** sZCleanCaption=" + sZCleanCaption + "|");
        }

        //TableQuery<wga_hu> XOrd_results = results.Clone<wga_hu>().OrderBy<float>(row => getFieldValue(sXCleanCaption, row));
        //TableQuery<wga_hu> YOrd_results = results.Clone<wga_hu>().OrderBy<float>(row => getFieldValue(sYCleanCaption, row));
        //TableQuery<wga_hu> ZOrd_results = results.Clone<wga_hu>().OrderBy<float>(row => getFieldValue(sZCleanCaption, row));
        //Vector2 v2_MinMax_X = new Vector2(getFieldValue(sXCleanCaption, XOrd_results.First()), );

        if (bDebug) {
            byte[] bytes_suc = Encoding.ASCII.GetBytes("brightness_median");
            byte[] bytes_mal = Encoding.ASCII.GetBytes(sXCleanCaption);

            int j;
            for (j = 0; j < bytes_suc.Length; j++) Debug.Log("suc char[" + j + "]=" + (int)bytes_suc[j] + "|");
            for (j = 0; j < bytes_mal.Length; j++) Debug.Log("mal char[" + j + "]=" + (int)bytes_mal[j] + "|");


            Debug.Log("****** bytes_suc=" + bytes_suc + "|len=" + bytes_suc.Length);
            Debug.Log("****** bytes_mal=" + bytes_mal + "|len=" + bytes_mal.Length);
        }

        Vector3 v3_MinMaxLen_X = new Vector3(results.Min(sXCleanCaption), results.Max(sXCleanCaption), 0.0f);
        Vector3 v3_MinMaxLen_Y = new Vector3(results.Min(sYCleanCaption), results.Max(sYCleanCaption), 0.0f);
        Vector3 v3_MinMaxLen_Z = new Vector3(results.Min(sZCleanCaption), results.Max(sZCleanCaption), 0.0f);
        v3_MinMaxLen_X.z = v3_MinMaxLen_X.y - v3_MinMaxLen_X.x;
        v3_MinMaxLen_Y.z = v3_MinMaxLen_Y.y - v3_MinMaxLen_Y.x;
        v3_MinMaxLen_Z.z = v3_MinMaxLen_Z.y - v3_MinMaxLen_Z.x;
        if (bDebug) {
            Debug.Log("***** v3_MinMaxLen_X=" + string.Format("{0:0.0000}", v3_MinMaxLen_X.x) + ", " + string.Format("{0:0.0000}", v3_MinMaxLen_X.y) + ", " + string.Format("{0:0.0000}", v3_MinMaxLen_X.z) + "|");
            Debug.Log("***** v3_MinMaxLen_Y=" + string.Format("{0:0.0000}", v3_MinMaxLen_Y.x) + ", " + string.Format("{0:0.0000}", v3_MinMaxLen_Y.y) + ", " + string.Format("{0:0.0000}", v3_MinMaxLen_Y.z) + "|");
            Debug.Log("***** v3_MinMaxLen_Z=" + string.Format("{0:0.0000}", v3_MinMaxLen_Z.x) + ", " + string.Format("{0:0.0000}", v3_MinMaxLen_Z.y) + ", " + string.Format("{0:0.0000}", v3_MinMaxLen_Z.z) + "|");
        }

        go_ejes_flechas.transform.position = new Vector3((-0.5f) * sm_inst.XDimensionSlider.value,
                                                           (-0.5f) * sm_inst.YDimensionSlider.value,
                                                           (-0.5f) * sm_inst.ZDimensionSlider.value);

        //Eliminar los girasoles previos
        foreach (Transform child in parent_origen.transform) {
            GameObject.Destroy(child.gameObject);
        }

        int dummy = 0;
        Debug.Log("*************** INICIO LoadImages() " + results.Count() + " **************");
        foreach (wga_hu wgahu_row_inst in results) {

            //Debug.Log("*** RAM=" + SystemInfo.systemMemorySize + " MB | VRAM=" + SystemInfo.graphicsMemorySize + " MB");

            dummy++;

            if (bDebug) Debug.Log("************ Img num = " + dummy);

            if (dSampleDistr > 0.0f) {
                if (rnd.NextDouble() > dSampleDistr) {
                    if (bDebug) Debug.Log("**** Decartado " + dummy);
                    continue;
                }
            }

            if (bEsfera) {
                int ir, ig, ib;
                ColorConverter.HsvToRgb(wgahu_row_inst.hue_mean * 360.0f / 255.0f,
                                        wgahu_row_inst.saturation_mean / 255.0f,
                                        wgahu_row_inst.brightness_mean / 255.0f,
                                        out ir,
                                        out ig,
                                        out ib);
                Material mat_inst = new Material(ori_esfera_material);
                mat_inst.color = new Color(((float)ir) / 255.0f,
                                           ((float)ig) / 255.0f,
                                           ((float)ib) / 255.0f);

                GameObject go_dummy_esfera = Instantiate<GameObject>(ori_esfera, parent_origen.transform);
                go_dummy_esfera.GetComponent<Renderer>().material = mat_inst;

                go_dummy_esfera.transform.localScale = new Vector3(fScaleFactor, fScaleFactor, fScaleFactor);

                gameObjectPositionActivate(go_dummy_esfera,
                                           sXCleanCaption, sYCleanCaption, sZCleanCaption,
                                           wgahu_row_inst,
                                           v3_MinMaxLen_X, v3_MinMaxLen_Y, v3_MinMaxLen_Z,
                                           sm_inst);

                yield return go_dummy_esfera;
            }
            else {

                //Heisenberg
                //Debug.Log(commandLineOutput("/system/bin/free", "-m"));


                string pathTemp = constantes.IMAGES_PATHPREFIX + constantes.IMAGES_PATH + wgahu_row_inst.filename;
                //Debug.Log("*********** pathTemp=" + pathTemp + "|");
                WWW www = new WWW(pathTemp);

                yield return www;

                //PENDIENTE ensayar ult argum = true, para creacion de mipmaps
                //PENDIENTE ensayar reduccion de width y height

                Texture2D texTmp = new Texture2D(www.texture.width, www.texture.height, TextureFormat.ASTC_RGB_4x4, false);
                //texTmp.Resize((int) ((float) www.texture.width / (float) nDivisorReducTextura), (int) ((float) www.texture.height / (float) nDivisorReducTextura), texTmp.format, false);

                // Texture2D texTmp = new Texture2D(www.texture.width, www.texture.height);

                //Texture2D texTmp = new Texture2D(www.texture.width / nDivisorReducTextura, www.texture.height / nDivisorReducTextura, TextureFormat.ASTC_RGB_12x12, false);

                /*texTmp.Resize(www.texture.width / nDivisorReducTextura, www.texture.height / nDivisorReducTextura);
                texTmp.Compress(false);
                texTmp.Apply();*/

                //Debug.Log("******************** ANTES DE LOADIMAGE " + texTmp.ToString());
                www.LoadImageIntoTexture(texTmp);

                /*texTmp.Resize(www.texture.width / nDivisorReducTextura, www.texture.height / nDivisorReducTextura);
                texTmp.Compress(false);
                texTmp.Apply();*/

                GameObject go_dummy = Instantiate<GameObject>(ori_quad, parent_origen.transform);
                go_dummy.GetComponent<Renderer>().material.SetTexture("_MainTex", texTmp);

                //TEMPORAL prueba de despliegue
                //System.Random rnd = new System.Random(getTime());
                //go_dummy.transform.position = new Vector3((((float) rnd.NextDouble()) - 0.5f) * fEspacioLen, (((float)rnd.NextDouble()) - 0.5f) * fEspacioLen, (((float)rnd.NextDouble()) - 0.5f) * fEspacioLen);

                go_dummy.transform.localScale = new Vector3(fScaleFactor, ((float)www.texture.height / (float)www.texture.width) * fScaleFactor, fScaleFactor);

                gameObjectPositionActivate(go_dummy,
                                           sXCleanCaption, sYCleanCaption, sZCleanCaption,
                                           wgahu_row_inst,
                                           v3_MinMaxLen_X, v3_MinMaxLen_Y, v3_MinMaxLen_Z,
                                           sm_inst);

            }

    }

        if (bDebug) Debug.Log("*************** Fin LoadImages() ");
    }

    private void gameObjectPositionActivate(GameObject go_inst,
                                            string sXCleanCaption, string sYCleanCaption, string sZCleanCaption, 
                                            wga_hu wgahu_row_inst,
                                            Vector3 v3_MinMaxLen_X, Vector3 v3_MinMaxLen_Y, Vector3 v3_MinMaxLen_Z, 
                                            settings_manager sm_inst) {

        go_inst.GetComponent<clickable_inst>().filename = wgahu_row_inst.filename;

        //Extrapolación
        Vector3 v3_dummy_pos = new Vector3(
                      (getFieldValue(sXCleanCaption, wgahu_row_inst) - v3_MinMaxLen_X.x) / v3_MinMaxLen_X.z,
                      (getFieldValue(sYCleanCaption, wgahu_row_inst) - v3_MinMaxLen_Y.x) / v3_MinMaxLen_Y.z,
                      (getFieldValue(sZCleanCaption, wgahu_row_inst) - v3_MinMaxLen_Z.x) / v3_MinMaxLen_Z.z
                );

        go_inst.transform.position = new Vector3((v3_dummy_pos.x - 0.5f) * sm_inst.XDimensionSlider.value,
                                                  (v3_dummy_pos.y - 0.5f) * sm_inst.YDimensionSlider.value,
                                                  (v3_dummy_pos.z - 0.5f) * sm_inst.ZDimensionSlider.value);

        go_inst.SetActive(true);
    }

    private int getTime() {
        int retval = 0;
        DateTime st = new DateTime(1970, 1, 1);
        DateTime univ = DateTime.Now.ToUniversalTime();
        TimeSpan t = (univ - st);
        double d = t.TotalMilliseconds;
        retval = (int)((d + 0.5) % Int32.MaxValue);
        return retval;
    }

    private string commandLineOutput(string comando, string argums) {
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo = new System.Diagnostics.ProcessStartInfo(comando, argums);

        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.UseShellExecute = false;

        p.Start();
        //p.WaitForExit();

        // instead of p.WaitForExit(), do
        StringBuilder q = new StringBuilder();
        while (!p.HasExited)
        {
            q.Append(p.StandardOutput.ReadToEnd());
        }

        return q.ToString();
    }

    private float getFieldValue(string fieldname, wga_hu row) {
        switch (fieldname) {
            case constantes.WGAHU_FIELD_HUE_MEAN:
                return row.hue_mean;
            case constantes.WGAHU_FIELD_HUE_MEDIAN:
                return row.hue_median;
            case constantes.WGAHU_FIELD_HUE_STDDEV:
                return row.hue_stddev;
            case constantes.WGAHU_FIELD_SATURATION_MEAN:
                return row.saturation_mean;
            case constantes.WGAHU_FIELD_SATURATION_MEDIAN:
                return row.saturation_median;
            case constantes.WGAHU_FIELD_SATURATION_STDDEV:
                return row.saturation_stddev;
            case constantes.WGAHU_FIELD_BRIGHTNESS_MEAN:
                return row.brightness_mean;
            case constantes.WGAHU_FIELD_BRIGHTNESS_MEDIAN:
                return row.brightness_median;
            case constantes.WGAHU_FIELD_BRIGHTNESS_STDDEV:
                return row.brightness_stddev;
            case constantes.WGAHU_FIELD_HUE_ENTROPY:
                return row.hue_entropy;
            case constantes.WGAHU_FIELD_SATURATION_ENTROPY:
                return row.saturation_entropy;
            case constantes.WGAHU_FIELD_BRIGHTNESS_ENTROPY:
                return row.brightness_entropy;
            case constantes.WGAHU_FIELD_DATE_NUM:
                return row.date_num;
        }

        return 0.0f;
    }

    private string cleanCaption(string s) {
        string cleanstr = s;
        cleanstr = cleanstr.Replace("\u000D", "");
        cleanstr = cleanstr.Replace("\u000A", "");
        cleanstr = cleanstr.Replace("\u0009", "");
        return cleanstr;
    }
}
