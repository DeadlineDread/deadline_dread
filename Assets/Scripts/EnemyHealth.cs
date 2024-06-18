using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBar; // 체력 바 이미지

    // 최대 체력 설정 메서드
    public void SetMaxHealth(int maxHealth)
    {
        healthBar.fillAmount = 1f; // 처음에는 체력 바가 가득 차있음
    }

    // 현재 체력 업데이트 메서드
    public void SetHealth(int currentHealth, int maxHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth; // 현재 체력 비율 계산
        healthBar.fillAmount = healthPercent; // 체력 바 비율 업데이트
    }
}