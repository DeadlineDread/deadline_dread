using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public int damage = 20; // 데미지 변수 추가

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 대상이 적인지 확인
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // 적의 체력을 감소시킴
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject); // 총알 삭제
    }
}
