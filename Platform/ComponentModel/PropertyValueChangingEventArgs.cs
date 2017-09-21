using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Platform
{
    /// <summary>
    /// Arguments for a property change event
    /// </summary>
    public class PropertyValueChangingEventArgs : PropertyChangingEventArgs
    {
        private object _NewValue;
        private object _OldValue;

        /// <summary>
        /// Constructs a property change event arguments
        /// </summary>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        public PropertyValueChangingEventArgs(string name, object oldvalue, object newvalue)
            : base( name )
        {
            _OldValue = oldvalue;
            _NewValue = newvalue;
        }

        /// <summary>
        /// Gets the new value
        /// </summary>
        public object NewValue  { get { return _NewValue; } }

        /// <summary>
        /// Gets the old value
        /// </summary>
        public object OldValue  { get { return _OldValue; } }
    }
}
