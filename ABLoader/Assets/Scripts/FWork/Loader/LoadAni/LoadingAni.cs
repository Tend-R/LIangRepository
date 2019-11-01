using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAni : MonoBehaviour
{   private GameObject loadImage;
    IEnumerator enumerator = null;
    float rotateSpeed = 2;
    private void Update()
    {

    }
    private void OnEnable()
    {
        loadImage = gameObject.transform.GetChild(0).gameObject;
        enumerator = rotateIenumrator();
        StartCoroutine(enumerator);
    }
    /// <summary>
    /// 协程实现旋转动画
    /// </summary>
    /// <returns></returns>
    IEnumerator rotateIenumrator()
    {
        while (true)
        {
            loadImage.transform.Rotate(0,0, rotateSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDisable()
    {
        if(enumerator!=null)
          StopCoroutine(enumerator);
    }
}
