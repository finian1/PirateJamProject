using UnityEngine;
using UnityEngine.EventSystems;

public interface IInteractionEvents : IEventSystemHandler
{
    void Damage(float amount, GameObject origin);
}
