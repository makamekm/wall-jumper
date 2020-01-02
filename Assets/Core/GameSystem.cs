using System;
using System.Collections.Generic;

public class GameSystem
{
    public GameLevel Level { get; set; }
    public GameManagerState GameManager { get; set; }
    public virtual IEnumerable<IDisposable> Subscriptions { get; protected set; } = null;
    public virtual void Update()
    { }
    public virtual void LateUpdate()
    { }
    public virtual void Init()
    { }
    public virtual void Start()
    { }
    public virtual void OnDestroy()
    { }
}
