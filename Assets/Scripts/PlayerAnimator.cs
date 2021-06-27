
using System.Collections.Generic;

using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
	public WeaponAnimations[] weaponAnimations;

	private Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

	protected override void Start()
	{
		base.Start();
		EquipmentManager.instance.onEquipmentChangedCallback += OnEquipmentChanged;

		weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
		foreach (var weaponAnims in weaponAnimations)
		{
			weaponAnimationsDict.Add(weaponAnims.weapon, weaponAnims.clips);
		}
	}

	private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
	{
		if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
		{
			animator.SetLayerWeight(1, 1);
			if (weaponAnimationsDict.ContainsKey(newItem))
			{
				currentAttackAnimSet = weaponAnimationsDict[newItem];
			}
		}
		else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
		{
			animator.SetLayerWeight(1, 0);
			currentAttackAnimSet = defaultAttackAnimSet;
		}

		if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
		{
			animator.SetLayerWeight(2, 1);
		}
		else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield)
		{
			animator.SetLayerWeight(2, 0);
		}
	}

	[System.Serializable]
	public struct WeaponAnimations
	{
		public Equipment weapon;
		public AnimationClip[] clips;
	}
}
