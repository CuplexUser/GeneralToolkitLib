using System;

namespace GeneralToolkitLib.Events
{
    public class BorderChangedEventArgs : EventArgs
    {
        private readonly TypeOfBorderUpdate _updateType;
        public BorderChangedEventArgs(TypeOfBorderUpdate updatedProperty)
        {
            _updateType = updatedProperty;

        }

        public TypeOfBorderUpdate UpdatedPorperty { get => _updateType; }


        public enum TypeOfBorderUpdate
        {
            BorderStyleChanged,
            InnerBorderColorChanged,
            OuterBorderColorChanged,
            InnerBorderWithChanged,
            OuterBorderWithChanged,
            ControlResized,
        }
    }
}
