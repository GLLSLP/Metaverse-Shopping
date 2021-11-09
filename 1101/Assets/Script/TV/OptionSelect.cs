using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelect : MonoBehaviour
{
    public GameObject obj;
    public bool isClicked = false;
    
    // �� ��ư�� �׷�
    public static string type = "";
    public static string size = "";
    public static string quailty = "";

    void Start()
    {

    }

    void Update()
    {
        Cursor.visible = true;
        Debug.Log(type + ", " + size + ", " + quailty + " ���� �Ϸ�");
    }

    public void ButtonClick()
    {
        isClicked = true;
        string name = this.gameObject.name;
        
        string parent = transform.parent.gameObject.name;
        
        if (parent.Equals("Type"))
            type = name;

        else if (parent.Equals("Size"))
            size = name;

        else if (parent.Equals("Quality"))
            quailty = name;
    }
}
