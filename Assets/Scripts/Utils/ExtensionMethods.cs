using UnityEngine;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
namespace Utils
{
    public static class ExtensionMethods
    {
        //Even though they are used like normal methods, extension
        //methods must be declared static. Notice that the first
        //parameter has the 'this' keyword followed by a Transform
        //variable. This variable denotes which class the extension
        //method becomes a part of.
        public static void DestroyAllChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; --i) {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
            transform.DetachChildren();
        }
    }
}