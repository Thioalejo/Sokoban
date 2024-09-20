using UnityEngine;

namespace Game.Sokoban
{
    public interface IInteractable
    {   //Trigger para detectar si hay un Dot
        void Trigger();
        void AddParent(Transform parent);
        void RemoveParent();
    }
}
