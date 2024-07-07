using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar3 : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    private Camera camera;

    public void UpdateHealthBar(float currentValue, float maxValue){
        slider.value = currentValue / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        camera = Camera.main;
        transform.rotation = camera.transform.rotation;
    }
}
