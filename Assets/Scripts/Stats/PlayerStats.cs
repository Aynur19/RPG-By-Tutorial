public class PlayerStats : CharacterStats
{
	private void Start()
	{
		EquipmentManager.instance.onEquipmentChangedCallback += OnEquipmentChanged;
	}

	private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
	{
		if (newItem != null)
		{
			armor.AddModifier(newItem.armorModifier);
			damage.AddModifier(newItem.damageModifier);

		}

		if (oldItem != null)
		{
			armor.RemoveModifier(oldItem.armorModifier);
			damage.RemoveModifier(oldItem.damageModifier);
		}
	}

	public override void Die()
	{
		base.Die();

		PlayerManager.instance.KillPlayer();
	}
}
