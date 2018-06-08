public enum BattleActionType 
{ 
    Bash, 
    Immobilize
}

public enum BattleActionResult
{ 
    Unresolved, 
    Successful, 
    Failed, 
    Dodged, 
    Smash 
}

public enum ItemType 
{ 
    Weapon, 
    Arms, 
    Body, 
    OtherEquippable, 
    StatBooster, 
    BattleItem, 
    Food, 
    Medicine, 
    Required, 
    Broken, 
    Miscellaneous 
};

public enum CharacterName 
{ 
    Any, 
    Ness, 
    Paula, 
    Jeff, 
    Poo 
};

public enum PermanentStatusAilment 
{
    Normal, 
    Unconscious, 
    Diamondized, 
    Paralyzed, 
    Nauseous, 
    Poisoned, 
    Sunstroke, 
    Cold 
}

public enum InBattleStatusAilment 
{ 
    Asleep, 
    Crying, 
    Immobilized, 
    Solidified 
}

public enum CoexistingStatusAilment 
{
    Normal, 
    FeelingStrange, 
    UnableToConcentrate, 
    Homesick 
}

public enum Stats 
{ 
    Offense, 
    Defense, 
    Speed, 
    Guts, 
    Luck, 
    Vitality, 
    IQ 
}
