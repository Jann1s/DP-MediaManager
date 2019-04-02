using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.ViewController
{
    interface ISubject
    {
        void Register(IObserver observer);
        void Unregister(IObserver observer);
        void Notify();
    }
}
