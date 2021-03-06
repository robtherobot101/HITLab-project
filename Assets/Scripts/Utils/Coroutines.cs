using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class Coroutines
    {
// run various routines, one after the other
        public static IEnumerator OneAfterTheOther( params IEnumerator[] routines ) 
        {
            foreach ( var item in routines ) 
            {
                while ( item.MoveNext() ) yield return item.Current;
            }
        }
    }
}
