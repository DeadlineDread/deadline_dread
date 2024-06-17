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
        currentHealth = maxHealth; // 시작 시 최대 체력으로 설정
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // 이미 사망 상태면 데미지 처리하지 않음

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0; // 체력이 0 이하로 내려가지 않도록 보정
            isDead = true;

            // 게임 오버 처리 함수 호출
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f); // 페이드 아웃 딜레이
        float fadeOutStartTime = Time.time;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - fadeOutStartTime) / fadeOutDuration);
            // 페이드 아웃 효과 처리 (예시)
            // 이 부분에서는 화면을 어둡게 만드는 등의 효과를 구현할 수 있습니다.
            yield return null;
        }

        // Game Over 씬으로 이동
        SceneManager.LoadScene(gameOverSceneName);
    }
}
