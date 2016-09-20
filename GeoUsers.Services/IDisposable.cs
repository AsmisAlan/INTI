using System;

namespace GeoUsers.Services
{
    public class OnDisposeAction : IDisposable
    {
        Action action;
        protected Action Action
        {
            get { return action ?? (() => { }); }
            set { action = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disposable"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public OnDisposeAction(Action action)
        {
            //action.ThrowIfNull("action");

            Action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnDisposeAction"/> class.
        /// </summary>
        protected OnDisposeAction()
            : this(() => { })
        {
        }

        bool disposed;

        /// <summary>
        /// Performs the task defined for this Disposable when it was created.
        /// </summary>
        public void Dispose()
        {
            if (disposed) throw new InvalidOperationException("Disposable action was already performed, the object is now disposed");

            Action();
            disposed = true;
        }
    }
}
