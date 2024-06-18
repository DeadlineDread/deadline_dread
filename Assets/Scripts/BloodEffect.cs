using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Image bloodImage; // �ǰ� ����� �̹���
    public PlayerHealth playerHealth; // �÷��̾��� ü���� �����ϴ� ��ũ��Ʈ
    public Color baseColor = new Color(150f / 255f, 0f, 0f); // �⺻ ���� (R: 150, G: 0, B: 0)
    public float baseAlpha = 0f; // �⺻ ����

    void Start()
    {
        // playerHealth�� null�̸� �÷��̾� ��ü���� PlayerHealth ������Ʈ�� ã�� �Ҵ�
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

        float currentHealth = playerHealth.currentHealth; // ���� �÷��̾��� ü�� ��������

        // �⺻ ����� ���� ����
        Color targetColor = baseColor;
        float targetAlpha = baseAlpha;

        // �÷��̾��� ü�¿� ���� ����� ���� ����
        if (currentHealth <= 50)
        {
            // ü���� 50 ������ ��
            targetColor = new Color(100f / 255f, 0f, 0f); // ������ �� ��Ӱ�
            targetAlpha = 0.94f; // ������ 200�� �ش��ϴ� ������ ���� (0.78)
        }
        else if (currentHealth <= 200)
        {
            // ü���� 50 �ʰ����� 200 ������ ��
            targetAlpha = 0.94f; // ������ 200�� �ش��ϴ� ������ ���� (0.78)
        }
        else if (currentHealth <= 500)
        {
            // ü���� 200 �ʰ����� 400 ������ ��
            targetAlpha = 0.59f; // ������ 150�� �ش��ϴ� ������ ���� (0.59)
        }
        else if (currentHealth <= 700)
        {
            // ü���� 600 �ʰ����� 700 ������ ��
            targetAlpha = 0.20f; // ������ 50�� �ش��ϴ� ������ ���� (0.20)
        }

        // ���ο� ����� ���� ����
        bloodImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, targetAlpha);
    }
}
