using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBar; // ü�� �� �̹���

    // �ִ� ü�� ���� �޼���
    public void SetMaxHealth(int maxHealth)
    {
        healthBar.fillAmount = 1f; // ó������ ü�� �ٰ� ���� ������
    }

    // ���� ü�� ������Ʈ �޼���
    public void SetHealth(int currentHealth, int maxHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth; // ���� ü�� ���� ���
        healthBar.fillAmount = healthPercent; // ü�� �� ���� ������Ʈ
    }
}