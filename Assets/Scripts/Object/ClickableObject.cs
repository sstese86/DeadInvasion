using UnityEngine;


namespace NaviEnt
{
    public abstract class ClickableObject : MonoBehaviour
    {

        private void OnMouseUp()
        {
            OnClicked();
        }

        public virtual void OnClicked()
        {

        }


    }
}