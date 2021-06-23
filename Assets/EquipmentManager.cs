using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	#region Singleton
	public static EquipmentManager instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;

	private Equipment[] currentEquipment;
	private Inventory inventory;

	private void Start()
	{
		inventory = Inventory.instance;

		var numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
	}

	public void Equip(Equipment newItem)
	{
		var slotIndex = (int)newItem.equipSlot;

		Equipment oldItem = null;

		if (currentEquipment[slotIndex] != null)
		{
			oldItem = currentEquipment[slotIndex];
			inventory.Add(oldItem);
		}

		onEquipmentChanged?.Invoke(newItem, oldItem);

		currentEquipment[slotIndex] = newItem;
	}

	public void Unequip(int slotIndex)
	{
		if (currentEquipment[slotIndex] != null)
		{
			Equipment oldItem = currentEquipment[slotIndex];
			inventory.Add(oldItem);

			currentEquipment[slotIndex] = null;
	
			onEquipmentChanged?.Invoke(null, oldItem);
		}
	}

	public void UnequipAll()
	{
		for (int i = 0; i < currentEquipment.Length; i++)
		{
			Unequip(i);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			UnequipAll(); 
		}	
	}
}
