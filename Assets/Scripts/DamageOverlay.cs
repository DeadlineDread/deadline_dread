using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    public Image bloodOverlay; // 피가 묻은 오버레이 이미지

    private int health; // 플레이어의 체력
    private int maxHealth = 1000; // 플레이어의 최대 체력

    void Start()
    {
        health = maxHealth;

        // 초기화
        UpdateBloodOverlay();
    }

    // 피격 효과 업데이트
    public void UpdateBloodOverlay()
    {
        float healthRatio = (float)health / maxHealth;

        // 체력에 따라 피가 묻은 오버레이 투명도 설정
        bloodOverlay.color = new Color(1f, 1f, 1f, 1f - healthRatio); // 체력이 낮을수록 불투명도 증가
    }

    // 플레이어 체력 설정
    public void SetPlayerHealth(int newHealth)
    {
        health = Mathf.Clamp(newHealth, 0, maxHealth);
        UpdateBloodOverlay();
    }
}
