using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
	public GameObject uiPrefab;
	public Transform target;

	private Transform mainCamera;
	private Transform ui;
	private Image healthSlider;
	private float visibleTime = 5;
	private float lastMadeVisibleTime;

	private void Start()
	{
		mainCamera = Camera.main.transform;
		foreach (var canvas in FindObjectsOfType<Canvas>())
		{
			if (canvas.renderMode == RenderMode.WorldSpace)
			{
				ui = Instantiate(uiPrefab, canvas.transform).transform;
				healthSlider = ui.GetChild(0).GetComponent<Image>();

				break;
			}
		}

		GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
	}

	private void LateUpdate()
	{
		if (ui != null)
		{
			ui.position = target.position;
			ui.forward = -mainCamera.forward;

			if (Time.time - lastMadeVisibleTime > visibleTime)
			{
				ui.gameObject.SetActive(false);
			}
		}
	}

	private void OnHealthChanged(int maxHeath, int currentHealth)
	{
		if (ui != null)
		{
			ui.gameObject.SetActive(true);
			lastMadeVisibleTime = Time.time;

			var healthPercent = currentHealth / (float)maxHeath;
			healthSlider.fillAmount = healthPercent;

			if (currentHealth <= 0)
			{
				Destroy(ui.gameObject);
			}
		}
	}
}
