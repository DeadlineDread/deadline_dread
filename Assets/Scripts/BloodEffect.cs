using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Image bloodImage; // 피가 묻어나는 이미지
    public PlayerHealth playerHealth; // 플레이어의 체력을 관리하는 스크립트
    public Color baseColor = new Color(150f / 255f, 0f, 0f); // 기본 색상 (R: 150, G: 0, B: 0)
    public float baseAlpha = 0f; // 기본 투명도

    void Start()
    {
        // playerHealth가 null이면 플레이어 객체에서 PlayerHealth 컴포넌트를 찾아 할당
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        UpdateBloodEffect();
    }

    void UpdateBloodEffect()
    {
        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth script is not assigned to BloodEffect script.");
            return;
        }

        float currentHealth = playerHealth.currentHealth; // 현재 플레이어의 체력 가져오기

        // 기본 색상과 투명도 설정
        Color targetColor = baseColor;
        float targetAlpha = baseAlpha;

        // 플레이어의 체력에 따라 색상과 투명도 조절
        if (currentHealth <= 50)
        {
            // 체력이 50 이하일 때
            targetColor = new Color(100f / 255f, 0f, 0f); // 빨강을 좀 어둡게
            targetAlpha = 0.94f; // 투명도를 200에 해당하는 값으로 설정 (0.78)
        }
        else if (currentHealth <= 200)
        {
            // 체력이 50 초과에서 200 이하일 때
            targetAlpha = 0.94f; // 투명도를 200에 해당하는 값으로 설정 (0.78)
        }
        else if (currentHealth <= 500)
        {
            // 체력이 200 초과에서 400 이하일 때
            targetAlpha = 0.59f; // 투명도를 150에 해당하는 값으로 설정 (0.59)
        }
        else if (currentHealth <= 700)
        {
            // 체력이 600 초과에서 700 이하일 때
            targetAlpha = 0.20f; // 투명도를 50에 해당하는 값으로 설정 (0.20)
        }

        // 새로운 색상과 투명도 적용
        bloodImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, targetAlpha);
    }
}
