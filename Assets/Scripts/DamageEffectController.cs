using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamageEffectController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // 후처리 볼륨
    private ChromaticAberration chromaticAberration; // 크로매틱 이상
    private Grain grain; // 그레인
    private Vignette vignette; // 비네트

    private int health; // 플레이어의 체력
    private int maxHealth = 1000; // 플레이어의 최대 체력

    void Start()
    {
        health = maxHealth;

        // 후처리 효과를 사용할 Post-process Volume에서 컴포넌트 가져오기
        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            postProcessVolume.profile.TryGetSettings(out chromaticAberration);
            postProcessVolume.profile.TryGetSettings(out grain);
            postProcessVolume.profile.TryGetSettings(out vignette);
        }

        // 초기화
        UpdatePostProcessEffects();
    }

    // 피격 효과 업데이트
    public void UpdateDamageEffect()
    {
        float healthRatio = (float)health / maxHealth;

        // 체력에 따라 후처리 효과 업데이트
        chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, 1f - healthRatio); // 체력이 낮을수록 크로매틱 이상 강도 증가
        grain.intensity.value = Mathf.Lerp(0f, 1f, 1f - healthRatio); // 체력이 낮을수록 그레인 강도 증가
        vignette.intensity.value = Mathf.Lerp(0f, 0.5f, 1f - healthRatio); // 체력이 낮을수록 비네트 강도 증가
    }

    // 플레이어 체력 설정
    public void SetPlayerHealth(int newHealth)
    {
        health = Mathf.Clamp(newHealth, 0, maxHealth);
        UpdatePostProcessEffects();
    }

    // 후처리 효과 초기화
    private void UpdatePostProcessEffects()
    {
        UpdateDamageEffect(); // 피격 효과 업데이트
    }
}
