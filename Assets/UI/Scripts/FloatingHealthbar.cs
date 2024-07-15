using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
  [SerializeField] private Slider slider;
  [SerializeField] private Transform target;

  public void UpdateHealthBar(float currentValue,float maxValue){
    slider.value = currentValue/maxValue;
  }

  void Update(){
    
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y + 1.0f, target.position.z);
        }
  }
}
