using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SQLite4Unity3d;

namespace LB_sw {
    public class settings_manager : MonoBehaviour {

        /*
            [SerializeField] private bool m_IsOn;

            public bool isOn
            {
                get { return m_IsOn; }
                set
                {
                    Set(value);
                }
            }
         */

        public Dropdown PresetsDropdown;

        public Toggle ItemShapeGirasolToggle;
        public Toggle ItemShapeEsferaToggle;

        public Toggle IRSPerspectiveToggle;
        public Toggle IRSOrthogonalToggle;

        public Slider SamplePercSlider;

        public Slider ItemScaleSlider;

        public ListBox FormListbox;
        public ListBox TypeListbox;
        public ListBox SchoolListbox;
        public ListBox TimeframeListbox;
        public ListBox AuthorListbox;

        public Dropdown XFeatureDropdown;
        public Dropdown YFeatureDropdown;
        public Dropdown ZFeatureDropdown;

        public Slider XDimensionSlider;
        public Slider YDimensionSlider;
        public Slider ZDimensionSlider;

        public Toggle DateFromToggle;
        public Toggle DateUntilToggle;
        public Slider DateFromSlider;
        public Slider DateUntilSlider;

        public GameObject go_MRPrjController;

        public void StartGenerarGirasoles() {
            StartCoroutine(GenerarGirasoles());
        }

        private bool bDebug = false;

        private IEnumerator GenerarGirasoles() {

            girasol_inst.bEsEsquemaPerspectivo = true;
            if (IRSOrthogonalToggle.isOn) {
                girasol_inst.bEsEsquemaPerspectivo = false;
            }

            List<criterio> criterios_set = new List<criterio>();

            ProcesarListbox(criterios_set, constantes.WGAHU_FIELD_FORM, FormListbox);
            ProcesarListbox(criterios_set, constantes.WGAHU_FIELD_TYPE, TypeListbox);
            ProcesarListbox(criterios_set, constantes.WGAHU_FIELD_SCHOOL, SchoolListbox);
            ProcesarListbox(criterios_set, constantes.WGAHU_FIELD_TIMEFRAME, TimeframeListbox);
            ProcesarListbox(criterios_set, constantes.WGAHU_FIELD_AUTHOR, AuthorListbox);

            string[] lista_date_num = new string[2];
            lista_date_num[0] = "-1";
            lista_date_num[1] = "-1";
            if (DateFromToggle.isOn) {
                lista_date_num[0] = (int)DateFromSlider.value + "";
            }
            if (DateUntilToggle.isOn) {
                lista_date_num[1] = (int)DateUntilSlider.value + "";
            }
            if (DateFromToggle.isOn || DateUntilToggle.isOn) {
                criterios_set.Add(new criterio(constantes.WGAHU_FIELD_DATE_NUM, lista_date_num));
            }

            //ejecutar busqueda, obt resultados
            TableQuery<wga_hu> results = go_MRPrjController.GetComponent<ExistingDBScript>().query(criterios_set);
            //IEnumerable<wga_hu> results = go_MRPrjController.GetComponent<ExistingDBScript>().GetResultSetTest();

            Debug.Log("results=" + results.ToString() + "|");

            StartCoroutine(go_MRPrjController.GetComponent<girasol_manager>().LoadImages(results, this));

            //procesar y cuadrar Axes Features y Dimensions
            //reescribir girasol_manager.LoadImages()
            //(opc: procesar resto de valores del menú)

            yield return null;
        }

        private void ProcesarListbox(List<criterio> criterios_set, string nombre, ListBox lb_inst) {
            string[] lista_sel = lb_inst.GetClickedItems();

            if (bDebug) {
                Debug.Log("***** LEN=" + lista_sel.Length);
                for (int i = 0; i < lista_sel.Length; i++) {
                    Debug.Log("**** form=" + lista_sel[i] + "|");
                }
            }

            if (lista_sel.Length > 0) {
                criterios_set.Add(new criterio(nombre, lista_sel));
            }
        }

        public void applyPreset() {
            if (PresetsDropdown.value == 1) {
                FormListbox.SelectItemByName("12345678", true);
                TypeListbox.SelectItemByName("12345678", true);
                SchoolListbox.SelectItemByName("12345678", true);
                TimeframeListbox.SelectItemByName("12345678", true);
                AuthorListbox.SelectItemByName("12345678", true);

                XFeatureDropdown.value = 0;
                YFeatureDropdown.value = 0;
                ZFeatureDropdown.value = 0;

                ItemShapeGirasolToggle.isOn = true;
                ItemShapeEsferaToggle.isOn = false;

                IRSPerspectiveToggle.isOn = true;
                IRSOrthogonalToggle.isOn = false;

                SamplePercSlider.value = 100;
                ItemScaleSlider.value = 50;
                XDimensionSlider.value = 10;
                YDimensionSlider.value = 10;
                ZDimensionSlider.value = 10;

                DateFromToggle.isOn = false;
                DateFromSlider.value = 150;

                DateUntilToggle.isOn = false;
                DateUntilSlider.value = 1923;

                PresetsDropdown.value = 0;
            }
            else if (PresetsDropdown.value == 2) {
                AuthorListbox.SelectItemByName("GOGH, Vincent van", true);

                XFeatureDropdown.value = 9;//brightness_median
                YFeatureDropdown.value = 5; //saturation_median
                ZFeatureDropdown.value = 12;//date_num
            }
            else if (PresetsDropdown.value == 3) {
                XFeatureDropdown.value = 9;//brightness_median
                YFeatureDropdown.value = 5; //saturation_median
                ZFeatureDropdown.value = 12;//date_num
            }
            else if (PresetsDropdown.value == 4) {
                XFeatureDropdown.value = 8;//brightness_mean
                YFeatureDropdown.value = 10; //brightness_stddev
                ZFeatureDropdown.value = 12;//date_num
            }
            else if (PresetsDropdown.value == 5) {
                XFeatureDropdown.value = 10;//brightness_stddev
                YFeatureDropdown.value = 11; //brightness_entropy
                ZFeatureDropdown.value = 12;//date_num
            }
            else if (PresetsDropdown.value == 6) {
                XFeatureDropdown.value = 9;//brightness_median
                YFeatureDropdown.value = 5; //saturation_median
                ZFeatureDropdown.value = 1;//hue_median
            }
            else if (PresetsDropdown.value == 7) {
                XFeatureDropdown.value = 8;//brightness_mean
                YFeatureDropdown.value = 4; //saturation_mean
                ZFeatureDropdown.value = 0;//hue_mean
            }
            else if (PresetsDropdown.value == 8) {
                AuthorListbox.SelectItemByName("REMBRANDT Harmenszoon van Rijn", true);

                ItemShapeGirasolToggle.isOn = false;
                ItemShapeEsferaToggle.isOn = true;

                XFeatureDropdown.value = 9;//brightness_median
                YFeatureDropdown.value = 5; //saturation_median
                ZFeatureDropdown.value = 12;//date_num
            }
        }

    }
}
