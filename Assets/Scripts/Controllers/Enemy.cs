using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
	private PlayerManager playerManager;
	private CharacterStats myStats;

	private void Start()
	{
		playerManager = PlayerManager.instance;
		myStats = GetComponent<CharacterStats>();
	}

	public override void Interact()
	{
		base.Interact();

		var playerCombat = playerManager.player.GetComponent<CharacterCombat>();

		if (playerCombat != null)
		{
			playerCombat.Attack(myStats);
		}
	}
}
