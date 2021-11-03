using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoor : MonoBehaviour
{
    private Animator animator; // �ִϸ����͸� ȣ���� ����

    // 1. �Ÿ��� ���� �� ������Ʈ ���
    public GameObject door1;
    public GameObject avatar1;
    float objectDistance;

    private bool doorOpen;

    void Start() // ����
    {
        doorOpen = false; // �� ������ ���� ���·� �ʱ�ȭ

        // �ִϸ����� ������ �ִϸ����� ������Ʈ�� �Ҵ��Ѵ�.
        animator = GetComponent<Animator>();
        Debug.Log("start");
    }

    void Update()
    {
        // 2. �� ������Ʈ�� ��ǥ ���ϱ�
        Vector3 doorPos = door1.transform.position;
        Vector3 avatPos = avatar1.transform.position;

        // 3. �Ÿ� ���
        float width = doorPos.x - avatPos.x;
        float height = doorPos.z - avatPos.z;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);
        // Debug.Log("�Ÿ�: " + distance);

        if (distance <= 9 && !doorOpen)
        {
            Debug.Log("open");
            doorOpen = true;
            Doors("Open");
        }
        else if (distance > 9 && doorOpen)
        {
            Debug.Log("close");
            doorOpen = false;
            Doors("Close"); // Close �Ķ���ͷ� Trigger�� ����
        }
    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
    }
}