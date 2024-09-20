using UnityEngine;

namespace Game.Sokoban
{
    public interface IInteractable
    {
        void Trigger();
        void AddParent(Transform parent);
        void RemoveParent();
    }
}
