using UnityEngine;
using UnityEngine.EventSystems;

public interface IDamageableObject : IEventSystemHandler
{
    void Damage(float amount, GameObject origin);
}

public interface IInteractableObject : IEventSystemHandler
{
    void Interact(GameObject origin);
}
