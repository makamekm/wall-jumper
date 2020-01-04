using System.Collections.Generic;
using UniRx;
using System.Linq;

public class GameLevelState
{
    public float Height = 0;
    public float CurrentHeight = 0;
    public float CameraHeight = 0;
    public float MinimumOffsetHeight = 100;
    //public BehaviorSubject<int?> SelectedGroup { get; } = new BehaviorSubject<int?>(null);
    //public BehaviorSubject<ActionType[]> SelectedActions { get; } = new BehaviorSubject<ActionType[]>(null);
    //public BehaviorSubject<ActionType?> SelectedActionType { get; } = new BehaviorSubject<ActionType?>(null);
}
