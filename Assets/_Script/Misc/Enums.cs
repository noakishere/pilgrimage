public enum CardType
{
    Route,
    Resource,
    Ability
}

public enum EventCellType
{
    Start,
    Empty,
    Combat,
    Scheme,
    Shop,
    End
}

public enum GameState
{
    Idle,
    RouteSelection,
    Navigation,
    InEvent,
    InCombat,
    InDialogue
}
