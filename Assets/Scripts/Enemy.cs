using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // �� ĳ������ ü��

    public void TakeDamage(int damage)
    {
        health -= damage; // ������ ��ŭ ü�� ����

        // ���⿡ ü�� ���ҿ� ���� ó�� �߰� ����
        
    }

    public void Die()
    {
        // ��� ó�� �ִϸ��̼� ��� �Ǵ� ���� ������Ʈ ���� ���� ����
    }
}
