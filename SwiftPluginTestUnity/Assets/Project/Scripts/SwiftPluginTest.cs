using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class SwiftPluginTest : MonoBehaviour
{
#if UNITY_EDITOR_OSX
    private const string DLL_NAME = "libSwiftPlugin";
    private const string DLL_NAME_WRAPPER = "libSwiftPluginWrapper";
#elif UNITY_IOS
    private const string DLL_NAME = "__Internal";
    private const string DLL_NAME_WRAPPER = "__Internal";
#endif

#if UNITY_EDITOR_OSX || UNITY_IOS
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int calc(int a, int b);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr create_instance();

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int use_utility_add(IntPtr instance, int a, int b);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int use_utility_sub(IntPtr instance, int a, int b);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr use_utility_version(IntPtr instance);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr use_utility_decorate(IntPtr instance, string text);
    
    [DllImport(DLL_NAME_WRAPPER, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int add(int a, int b);

    [SerializeField] private TMP_Text _text1;
    [SerializeField] private TMP_Text _text2;
    [SerializeField] private TMP_Text _text3;
    [SerializeField] private TMP_Text _text4;
    [SerializeField] private TMP_Text _text5;

    private void Start()
    {
        {
            int result1 = calc(10, 20);

            Debug.Log(result1);

            _text1.text = result1.ToString();
        }

        {
            IntPtr instance = create_instance();
            int result2 = use_utility_add(instance, 25, 32);

            Debug.Log(result2);

            _text2.text = result2.ToString();

            IntPtr strPtr = use_utility_version(instance);
            string result3 = Marshal.PtrToStringAnsi(strPtr);

            Debug.Log(result3);

            _text3.text = result3;

            Marshal.FreeHGlobal(strPtr);
            
            IntPtr decoratedStrPtr = use_utility_decorate(instance, "Hello, Swift!");
            string result4 = Marshal.PtrToStringAnsi(decoratedStrPtr);

            Debug.Log(result4);

            _text4.text = result4;

            Marshal.FreeHGlobal(decoratedStrPtr);
        }

        {
            int result5 = add(100, 200);

            Debug.Log(result5);
            
            _text5.text = result5.ToString();
        }
    }
#endif
}