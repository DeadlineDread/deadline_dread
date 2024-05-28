using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 0.5f;

    private void Update()
    {
        // ��, �� ������
        float x = Input.GetAxis("Horizontal");
        // ��, �� ������
        float y = Input.GetAxis("Vertical");

        // �̵� ���� ����
        Vector3 move = transform.right * x + transform.forward * y;
        // �̵� �ӵ� ����
        controller.Move(move * speed * Time.deltaTime);
    }
}
