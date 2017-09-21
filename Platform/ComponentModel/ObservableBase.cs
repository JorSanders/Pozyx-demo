using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Platform.ComponentModel
{
    /// <summary>
    /// Data Bindable entity
    /// </summary>
    public class ObservableBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Signals a property is changing
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Send property changing
        /// </summary>
        /// <param name="name"></param>
        protected virtual void OnPropertyChanging(object sender, PropertyChangingEventArgs args)
        {
            var handler = PropertyChanging;
            if (handler != null) PropertyChanging(this, args);
        }

        /// <summary>
        /// Signals a property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Send property changed
        /// </summary>
        /// <param name="name"></param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null) PropertyChanged(this, args);
        }

        /// <summary>
        /// Set new property value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="newvalue"></param>
        /// <param name="name"></param>
        protected virtual bool SetPropertyValue<T>(ref T value, T newvalue, [CallerMemberName] string name = null )
        {
            // must have changed
            if (Object.Equals(value, newvalue)) return false;

            // create event arguments
            var args1 = new PropertyValueChangingEventArgs( name, value, newvalue );
            var args2 = new PropertyValueChangedEventArgs( name, value, newvalue );
            
            // signal property change
            OnPropertyChange(name, value, newvalue);

            // signal about to change
            OnPropertyChanging(this, args1);
            value = newvalue;
            OnPropertyChanged(this, args2);

            // return OK
            return true;
        }

        /// <summary>
        /// Set new virtual property value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        /// <param name="name"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        protected virtual bool SetPropertyValue<T>(T oldvalue, T newvalue, string name, Action<T> setter )
        {
            // must have changed
            if (Object.Equals(oldvalue, newvalue)) return false;

            // create event arguments
            var args1 = new PropertyValueChangingEventArgs(name, oldvalue, newvalue);
            var args2 = new PropertyValueChangedEventArgs(name, oldvalue, newvalue);

            // signal property change
            OnPropertyChange(name, oldvalue, newvalue);

            // signal about to change
            OnPropertyChanging(this, args1);
            setter?.Invoke(newvalue);
            OnPropertyChanged(this, args2);

            // return OK
            return true;
        }

        /// <summary>
        /// Set property value and notify dependend property changes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="value"></param>
        /// <param name="newvalue"></param>
        /// <param name="name"></param>
        /// <param name="depend1"></param>
        /// <param name="getter1"></param>
        /// <returns></returns>
        protected virtual bool SetPropertyValue<T, U>(ref T value, T newvalue, string name, string depend1, Func<U> getter1)
        {
            // must have changed
            if (Object.Equals(value, newvalue)) return false;

            // create event arguments
            var args1 = new PropertyValueChangingEventArgs(name, value, newvalue);
            var args2 = new PropertyValueChangedEventArgs(name, value, newvalue);

            // signal property change
            OnPropertyChange(name, value, newvalue);

            // get old value
            U oldvalue1 = getter1();

            // signal about to change
            OnPropertyChanging(this, args1);
            value = newvalue;
            OnPropertyChanged(this, args2);

            // feedback dependent property
            SetPropertyValue(oldvalue1, getter1(), depend1, null);

            // return OK
            return true;
        }

        /// <summary>
        /// Catch property changes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        protected virtual void OnPropertyChange<T>(string name, T oldvalue, T newvalue)
        {
        }
    }
}
