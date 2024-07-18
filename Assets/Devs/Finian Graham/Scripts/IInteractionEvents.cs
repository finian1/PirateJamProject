using UnityEngine.EventSystems;

public interface IInteractionEvents : IEventSystemHandler
{
    void Damage(float amount);
}
