using System.Collections;

using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

	public float attackDelay = 0.6f;
	public float attackSpeed = 1f;
	public event System.Action OnAttack;
	public bool InCombat { get; private set; }
	
	private const float combatCooldown = 5;
	private float attackCooldown = 0f;
	private float lastAttackTime;
	private CharacterStats myStats;

	private void Start()
	{
		myStats = GetComponent<CharacterStats>();
	}

	private void Update()
	{
		attackCooldown -= Time.deltaTime;

		if (Time.deltaTime - lastAttackTime > combatCooldown)
		{
			InCombat = false;
		}
	}

	public void Attack(CharacterStats targetStats)
	{
		if (attackCooldown <= 0)
		{
			StartCoroutine(DoDamage(targetStats, attackDelay));

			OnAttack?.Invoke();
			attackCooldown = 1 / attackSpeed;
			InCombat = true;
			lastAttackTime = Time.time;
		}
	}

	private IEnumerator DoDamage(CharacterStats stats, float delay)
	{
		yield return new WaitForSeconds(delay);
		stats.TakeDamage(myStats.damage.GetValue());
		if (stats.currentHealth <= 0)
		{
			InCombat = false;
		}
	}
}
