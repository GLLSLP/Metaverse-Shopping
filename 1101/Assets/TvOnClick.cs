using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvOnClick : MonoBehaviour
{
    // Ŭ�� �̺�Ʈ�� ������ ��ü
    public GameObject popup;
    public Camera testCam;
    // ���� 1. ī�޶� ����
    // public Camera mainCam;
    // public Camera subCam;
    //

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        // ���� �� ui ��� ī�޶�� �׿�����
       // subCam.enabled = true;
        popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("p"))
        {
            popup.SetActive(true);
        }
        */
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = testCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if(true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
            {
                popup.SetActive(true);
                // mainCam.enabled = true;
                // subCam.enabled = false;
            }
        }
    }
}
