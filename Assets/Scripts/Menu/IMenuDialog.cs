using UnityEngine;

namespace Pixelgrid
{
    public interface IMenuDialog
    {
        void Notify(string eventType);
        GameObject GetPanel();
    }
}
