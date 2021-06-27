using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAnimator : MonoBehaviour
{
	public AnimationClip replaceableAttackAnim;
	public AnimationClip[] defaultAttackAnimSet; 

	private const float locomotionAnimationSmoothTime = 0.1f;
	private NavMeshAgent agent;

	protected Animator animator;
	protected CharacterCombat combat;
	protected AnimatorOverrideController overrideController;
	protected AnimationClip[] currentAttackAnimSet;

	protected virtual void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		combat = GetComponent<CharacterCombat>();

		overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
		animator.runtimeAnimatorController = overrideController;

		currentAttackAnimSet = defaultAttackAnimSet;
		combat.OnAttack += OnAttack;
	}

	protected virtual void Update()
	{
		var speedPercent = agent.velocity.magnitude / agent.speed;
		animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
		animator.SetBool("inCombat", combat.InCombat);
	}

	protected virtual void OnAttack()
	{
		animator.SetTrigger("attack");
		var attackIndex = Random.Range(0, currentAttackAnimSet.Length);
		overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
	}
}
