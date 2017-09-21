using System;

namespace Platform.ComponentModel
{
    /// <summary>
    /// Disposable base object
    /// </summary>
    public class IDisposableBase : IDisposable
    {
        private bool _IsDisposed;

        /// <summary>
        /// Garbage collected
        /// </summary>
        ~IDisposableBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Request manual disposal
        /// </summary>
        void IDisposable.Dispose()
        {
            _IsDisposed = true;
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns true if object is disposed
        /// </summary>
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        /// <summary>
        /// Handle cleanup
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {            
        }
    }
}
