using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SQLite4Unity3d;

using LB_sw;

public class picdetails_manager : MonoBehaviour {

    public GameObject go_MRPrjController;

    public GameObject go_PicDetailsCanvas;
    public GameObject go_origen_objs;
    public GameObject go_ejes_flechas;

    public RawImage WgahuPictureRawImage;
    public Text CaptionAuthorBorndiedText;
    public Text CaptionTitleText;
    public Text CaptionSchoolText;
    public Text CaptionTimeframeText;
    public Text CaptionFormText;
    public Text CaptionTypeText;
    public Text CaptionDateText;
    public Text CaptionTechniqueText;
    public Text CaptionLocationText;

    public Camera camera_inst;
    public GameObject go_MenuButton;

    public float fPicWidth_ori;
    public float fPicHeight_ori;
    
    void Start() {
        fPicWidth_ori  = v2Pic().x;
        fPicHeight_ori = v2Pic().y;
    }

    private Vector2 v2Pic() {
        return WgahuPictureRawImage.GetComponent<RectTransform>().sizeDelta;
    }

    public void restaurarPictureDims() {
        WgahuPictureRawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(fPicWidth_ori, fPicHeight_ori);
    }

    private bool bDebug = false;

    public void LoadShowPicDetails(string filename) {

        wga_hu wgahu_row = go_MRPrjController.GetComponent<ExistingDBScript>().findByFilename(filename);

        CaptionAuthorBorndiedText.text = wgahu_row.author + " " + wgahu_row.borndied;
        CaptionTitleText.text = wgahu_row.title;
        CaptionSchoolText.text = wgahu_row.school;
        CaptionTimeframeText.text = wgahu_row.timeframe;
        CaptionFormText.text = wgahu_row.form;
        CaptionTypeText.text = wgahu_row.type;
        CaptionDateText.text = wgahu_row.date;
        CaptionTechniqueText.text = wgahu_row.technique;
        CaptionLocationText.text = wgahu_row.location;

        string pathTemp = constantes.IMAGES_PATHPREFIX + constantes.IMAGES_PATH + constantes.IMAGES_FULLDIRPREFIX + wgahu_row.filename;
        WWW www = new WWW(pathTemp);
        Texture2D texTmp = new Texture2D(www.texture.width, www.texture.height, TextureFormat.ASTC_RGB_4x4, false);
        www.LoadImageIntoTexture(texTmp);
        WgahuPictureRawImage.texture = texTmp;

        if (bDebug) Debug.Log("****** sizeDelta=" + v2Pic());

        float fRel1 = (float)www.texture.width / (float)www.texture.height;
        if (fRel1 > (fPicWidth_ori/ fPicHeight_ori)) {
            WgahuPictureRawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(v2Pic().x , v2Pic().x / fRel1);
            //WgahuPictureRawImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, v2Pic().x / fRel1);
        }
        else {
            WgahuPictureRawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(v2Pic().y * fRel1, v2Pic().y);
            //WgahuPictureRawImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, v2Pic().y * fRel1);
        }

        if (bDebug) Debug.Log("****** sizeDelta after=" + v2Pic());

        go_MenuButton.gameObject.SetActive(false);
        //camera_inst.cullingMask = 255;//todos excepto PicLayer y Gazable
        go_origen_objs.SetActive(false);
        go_ejes_flechas.SetActive(false);
        go_PicDetailsCanvas.SetActive(true);

    }

}
