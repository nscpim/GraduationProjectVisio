using UnityEngine;

public class AndroidInitializer : AndroidJavaProxy
{
    public AndroidInitializer(string javaInterface) : base(javaInterface)
    {
    }

    public AndroidInitializer(AndroidJavaClass javaInterface) : base(javaInterface)
    {
    }
}
