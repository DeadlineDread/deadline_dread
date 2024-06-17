using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float fadeOutDuration = 1.5f;
    public string gameOverSceneName = "GameOver";

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth; // ���� �� �ִ� ü������ ����
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // �̹� ��� ���¸� ������ ó������ ����

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0; // ü���� 0 ���Ϸ� �������� �ʵ��� ����
            isDead = true;

            // ���� ���� ó�� �Լ� ȣ��
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f); // ���̵� �ƿ� ������
        float fadeOutStartTime = Time.time;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - fadeOutStartTime) / fadeOutDuration);
            // ���̵� �ƿ� ȿ�� ó�� (����)
            // �� �κп����� ȭ���� ��Ӱ� ����� ���� ȿ���� ������ �� �ֽ��ϴ�.
            yield return null;
        }

        // Game Over ������ �̵�
        SceneManager.LoadScene(gameOverSceneName);
    }
}
