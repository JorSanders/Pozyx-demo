using System;

namespace Platform.ComponentModel
{
    /// <summary>
    /// A disposable base class
    /// </summary>
    public class DisposableBase : IDisposableBase
    {
        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)this).Dispose();
        }
    }
}
