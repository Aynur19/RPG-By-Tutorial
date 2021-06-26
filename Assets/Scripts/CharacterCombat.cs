using System.Collections;

using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
	public float attackDelay = 0.6f;
	public float attackSpeed = 1f;
	public event System.Action OnAttack;
	
	private float attackCooldown = 0f;
	private CharacterStats myStats;

	private void Start()
	{
		myStats = GetComponent<CharacterStats>();
	}

	private void Update()
	{
		attackCooldown -= Time.deltaTime;
	}

	public void Attack(CharacterStats targetStats)
	{
		if (attackCooldown <= 0)
		{
			StartCoroutine(DoDamage(targetStats, attackDelay));

			OnAttack?.Invoke();
			attackCooldown = 1 / attackSpeed;
		}
	}

	private IEnumerator DoDamage(CharacterStats stats, float delay)
	{
		yield return new WaitForSeconds(delay);
		stats.TakeDamage(myStats.damage.GetValue());
	}
}
