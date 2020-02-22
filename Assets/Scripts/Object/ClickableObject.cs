using UnityEngine;


namespace NaviEnt
{
    public abstract class ClickableObject : MonoBehaviour
    {

        protected virtual void OnMouseUpAsButton()
        {
            FeedbackOnMouseUp();
        }

        public virtual void FeedbackOnMouseUp()
        {

        }
        public virtual void OnMouseDown()
        {
            
        }

    }
}