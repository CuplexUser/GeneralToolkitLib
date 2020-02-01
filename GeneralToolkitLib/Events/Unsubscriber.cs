using System;
using System.Collections.Generic;

namespace GeneralToolkitLib.Events
{
    public class Unsubscriber<T> where T : IDisposable
    {
        private readonly List<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
                if (default(T) != null)
                {
                    default(T).Dispose();
                }
            }
        }
    }
}