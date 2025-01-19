using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

enum PlayerActionTypes
{
    MOVE,
    ATTACK,
    COLLECT,
    DIE
}

interface IObserverGameElements
{
    // Forward an update from the subject elements
    // subject types are: (move / attack / collect / die)
    
    public void update(ISubjectGameEvents subjectGameEvents);
}

