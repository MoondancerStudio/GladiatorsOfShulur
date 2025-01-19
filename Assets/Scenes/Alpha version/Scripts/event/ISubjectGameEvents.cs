using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface ISubjectGameEvents
{
    void attach(IObserverGameElements observerGameElements);

    void deatach(IObserverGameElements observerGameElements);

    // virtual private List<IDispatcherGameElements> _dispatcherGameElements;
    void notifyAllGameElements();
}

