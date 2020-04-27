using UnityEngine;
using UnityEngine.UI;

namespace UI.CustomSelectable
{
    public class CustomSelectable : Selectable
    {
        [SerializeField]
        private Selectable upSelectable;

        [SerializeField]
        private Selectable downSelectable;

        [SerializeField]
        private Selectable leftSelectable;

        [SerializeField]
        private Selectable rightSelectable;

        public override Selectable FindSelectableOnUp()
        {
            return upSelectable != null ? upSelectable : base.FindSelectableOnUp();
        }

        public override Selectable FindSelectableOnDown()
        {
            return downSelectable != null ? downSelectable : base.FindSelectableOnDown();
        }

        public override Selectable FindSelectableOnLeft()
        {
            return leftSelectable != null ? leftSelectable : base.FindSelectableOnLeft();
        }

        public override Selectable FindSelectableOnRight()
        {
            return rightSelectable != null ? rightSelectable : base.FindSelectableOnRight();
        }
    }
}