public enum BattleActionType { Bash, Immobilize, BreakImmobilization };

public enum BattleActionResult { Successful, Failed, Dodged, Smash }

public enum ItemType { Weapon, Arms, Body, OtherEquippable, StatBooster, BattleItem, Food, Medicine, Required, Broken, Miscellaneous };

public enum CharacterName { Any, Ness, Paula, Jeff, Poo };

public enum PermanentStatusAilment { Unconscious, Diamondized, Paralyzed, Nauseous, Poisoned, Sunstroke, Cold }

public enum InBattleStatusAilment { Asleep, Crying, Immobilized, Solidified }

// We may not get a point where we use these, but I'm putting them in here just in case
public enum SpecialStatusAilment { Mushroomed, Possessed }

public enum CoexistingStatusAilment { FeelingStrange, UnableToConcentrate, Homesick }