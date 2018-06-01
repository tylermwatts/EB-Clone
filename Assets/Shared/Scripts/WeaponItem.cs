using System;

public class WeaponItem : Item 
{
	public WeaponItem(string itemName, ItemType itemType, int offense, int guts, int accuracy)
	{
        ItemName = itemName;
        ItemType = itemType;
        Offense = offense;
        Guts = guts;
        if (accuracy > 0 && accuracy <= 16)
        { 
            Accuracy = accuracy;
        }
        else
        {
            throw new ArgumentException("Accuracy must be between 1 and 16 inclusive.");
        }
    }
	
	public int Offense { get; }
	public int Guts { get; }
	public int Accuracy { get; }
}
