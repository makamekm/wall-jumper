using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInputManager
{
    private Dictionary<string, string> Config = new Dictionary<string, string>();
    public Dictionary<ClickSensorEntity, RaycastHit> CursorEntities { get; set; } = new Dictionary<ClickSensorEntity, RaycastHit>();
    public Dictionary<GameObject, EventSystem> CursorUIEntities { get; set; } = new Dictionary<GameObject, EventSystem>();

    public bool IsCursorBlocked { get; set; } = true;

    public void LateUpdate()
    {
        CursorEntities.Clear();
        CursorUIEntities.Clear();
        IsCursorBlocked = true;
    }

    public void Update() {
        CatchClick();
    }

    void CatchClick() {
        if (Input.GetMouseButtonUp(0))
        {
            IsCursorBlocked = false;
            var ray = GameManagerSingleton.Instance.Level.Camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                GameManagerSingleton.Instance.EntityManager.Single<ClickSensorEntity>(
                    hit.collider.gameObject.name == "ClickSensor"
                        ? hit.collider.transform.parent.gameObject
                        : hit.collider.gameObject,
                    (sensor) => {
                        if (!CursorEntities.ContainsKey(sensor))
                        {
                            CursorEntities.Add(sensor, hit);
                        }
                    }
                );
            }

            if (EventSystem.current.IsPointerOverGameObject()
                && EventSystem.current.currentSelectedGameObject != null)
            {
                CursorUIEntities.Add(EventSystem.current.currentSelectedGameObject, EventSystem.current);
            }
        }
    }
}
