using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    public Image bloodOverlay; // �ǰ� ���� �������� �̹���

    private int health; // �÷��̾��� ü��
    private int maxHealth = 1000; // �÷��̾��� �ִ� ü��

    void Start()
    {
        health = maxHealth;

        // �ʱ�ȭ
        UpdateBloodOverlay();
    }

    // �ǰ� ȿ�� ������Ʈ
    public void UpdateBloodOverlay()
    {
        float healthRatio = (float)health / maxHealth;

        // ü�¿� ���� �ǰ� ���� �������� ���� ����
        bloodOverlay.color = new Color(1f, 1f, 1f, 1f - healthRatio); // ü���� �������� ������ ����
    }

    // �÷��̾� ü�� ����
    public void SetPlayerHealth(int newHealth)
    {
        health = Mathf.Clamp(newHealth, 0, maxHealth);
        UpdateBloodOverlay();
    }
}
