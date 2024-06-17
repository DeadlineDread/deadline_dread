using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamageEffectController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // ��ó�� ����
    private ChromaticAberration chromaticAberration; // ũ�θ�ƽ �̻�
    private Grain grain; // �׷���
    private Vignette vignette; // ���Ʈ

    private int health; // �÷��̾��� ü��
    private int maxHealth = 1000; // �÷��̾��� �ִ� ü��

    void Start()
    {
        health = maxHealth;

        // ��ó�� ȿ���� ����� Post-process Volume���� ������Ʈ ��������
        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            postProcessVolume.profile.TryGetSettings(out chromaticAberration);
            postProcessVolume.profile.TryGetSettings(out grain);
            postProcessVolume.profile.TryGetSettings(out vignette);
        }

        // �ʱ�ȭ
        UpdatePostProcessEffects();
    }

    // �ǰ� ȿ�� ������Ʈ
    public void UpdateDamageEffect()
    {
        float healthRatio = (float)health / maxHealth;

        // ü�¿� ���� ��ó�� ȿ�� ������Ʈ
        chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, 1f - healthRatio); // ü���� �������� ũ�θ�ƽ �̻� ���� ����
        grain.intensity.value = Mathf.Lerp(0f, 1f, 1f - healthRatio); // ü���� �������� �׷��� ���� ����
        vignette.intensity.value = Mathf.Lerp(0f, 0.5f, 1f - healthRatio); // ü���� �������� ���Ʈ ���� ����
    }

    // �÷��̾� ü�� ����
    public void SetPlayerHealth(int newHealth)
    {
        health = Mathf.Clamp(newHealth, 0, maxHealth);
        UpdatePostProcessEffects();
    }

    // ��ó�� ȿ�� �ʱ�ȭ
    private void UpdatePostProcessEffects()
    {
        UpdateDamageEffect(); // �ǰ� ȿ�� ������Ʈ
    }
}
