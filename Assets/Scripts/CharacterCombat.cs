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
	private CharacterStats opponentStats;

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
			opponentStats = targetStats;
			OnAttack?.Invoke();
			attackCooldown = 1 / attackSpeed;
			InCombat = true;
			lastAttackTime = Time.time;
		}
	}

	public void AttackHit_AnimationEvent()
	{
		opponentStats.TakeDamage(myStats.damage.GetValue());
		if (opponentStats.currentHealth <= 0)
		{
			InCombat = false;
		}
	}
}
