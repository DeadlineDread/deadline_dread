using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public int damage = 20; // ������ ���� �߰�

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ����� ������ Ȯ��
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // ���� ü���� ���ҽ�Ŵ
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject); // �Ѿ� ����
    }
}
