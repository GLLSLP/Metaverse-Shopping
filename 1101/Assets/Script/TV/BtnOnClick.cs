using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnOnClick : MonoBehaviour
{
    // Ŭ�� �̺�Ʈ�� ������ ��ü
    public GameObject popup;
    public Camera testCam;
    
    private RaycastHit hit;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = testCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
            {
                popup.SetActive(true);
            }
        }

    }
}