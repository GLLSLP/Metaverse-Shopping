using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnOnClick : MonoBehaviour
{
    // Ŭ�� �̺�Ʈ�� ������ ��ü
    public GameObject popup;
    public Button btn;

    /*
    void Start()
    {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(fClick);
    }

    public void fClick()
    {
        popup.SetActive(false);
        Debug.Log("����");
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            popup.SetActive(false);
        }
    }

}
