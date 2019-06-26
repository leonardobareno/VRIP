using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_axis_manager : MonoBehaviour {

    public Text caption;

    public void cambiar_mostrar_valor()
    {
        caption.text = (int) gameObject.GetComponent<Slider>().value + "";
    }

    public void incr_1() {
        gameObject.GetComponent<Slider>().value++;
    }

    public void decr_1() {
        gameObject.GetComponent<Slider>().value--;
    }
}
