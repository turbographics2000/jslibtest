using UnityEngine;
using System.Runtime.InteropServices;
using System;
using AOT;
using System.Collections.Generic;
using System.Text;

public class jslibbridge : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void byteArrayTest(byte[] data, int size);

    [DllImport("__Internal")]
    private static extern void sbyteArrayTest(sbyte[] data, int size);

    [DllImport("__Internal")]
    private static extern void shortArrayTest(short[] data, int size);

    [DllImport("__Internal")]
    private static extern void ushortArrayTest(ushort[] data, int size);

    [DllImport("__Internal")]
    private static extern void intArrayTest(int[] data, int size);

    [DllImport("__Internal")]
    private static extern void uintArrayTest(uint[] data, int size);

    [DllImport("__Internal")]
    private static extern void floatArrayTest(float[] data, int size);

    [DllImport("__Internal")]
    private static extern void doubleArrayTest(double[] data, int size);

    delegate void dlgReceiveTextData(string txt, int len);
    delegate void dlgReceiveByteArrayData(byte[] data, int length);
    delegate void dlgReceiveSByteArrayData(sbyte[] data, int length);
    delegate void dlgReceiveShortArrayData(short[] data, int length);
    delegate void dlgReceiveUShortArrayData(ushort[] data, int length);
    delegate void dlgReceiveIntArrayData(int[] data, int length);
    delegate void dlgReceiveUIntArrayData(uint[] data, int length);
    delegate void dlgReceiveFloatArrayData(float[] data, int length);
    delegate void dlgReceiveDoubleArrayData(double[] data, int length);

    [DllImport("__Internal")]
    static extern uint initCallback(
        dlgReceiveTextData onText,
        dlgReceiveSByteArrayData onSByteArray,
        dlgReceiveByteArrayData onByteArray,
        dlgReceiveShortArrayData onShortArray,
        dlgReceiveUShortArrayData onUShortArray,
        dlgReceiveIntArrayData onIntArray,
        dlgReceiveUIntArrayData onUIntArray,
        dlgReceiveFloatArrayData onFloatArray,
        dlgReceiveDoubleArrayData onDoubleArray
    );

    [MonoPInvokeCallback(typeof(dlgReceiveTextData))]
    static void OnText(string txt, int len)
    {
        Debug.Log("recieve text: " + txt);
    }

    static void debuglogReceiveData<T>(string type, T[] data, int length)
    {
        var sb = new StringBuilder();
        sb.Append("Receive " + type + " array: (length: " + length + ") [");
        var spl = "";
        for (var i = 0; i < length; i++)
        {
            sb.Append(spl + data[i]);
            spl = ", ";
        }
        sb.Append("]");
        Debug.Log(sb.ToString());
    }

    [MonoPInvokeCallback(typeof(dlgReceiveSByteArrayData))]
    static void OnSByteArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 1)] sbyte[] data, int length)
    {
        debuglogReceiveData<sbyte>("sbyte", data, length);
    }

    [MonoPInvokeCallback(typeof(dlgReceiveByteArrayData))]
    static void OnByteArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] byte[] data, int length)
    {
        debuglogReceiveData<byte>("byte", data, length);
    }

    [MonoPInvokeCallback(typeof(dlgReceiveShortArrayData))]
    static void OnShortArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I2, SizeParamIndex = 1)] short[] data, int length)
    {
        debuglogReceiveData<short>("short", data, length);
    }

    [MonoPInvokeCallback(typeof(dlgReceiveUShortArrayData))]
    static void OnUShortArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 1)] ushort[] data, int length)
    {
        debuglogReceiveData<ushort>("ushort", data, length);
    }

    [MonoPInvokeCallback(typeof(dlgReceiveIntArrayData))]
    static void OnIntArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 1)] int[] data, int length)
    {
        debuglogReceiveData<int>("int", data, length);
    }

    [MonoPInvokeCallback(typeof(dlgReceiveUIntArrayData))]
    static void OnUIntArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 1)] uint[] data, int length)
    {
        debuglogReceiveData<uint>("uint", data, length);
    }

    [MonoPInvokeCallback(typeof(dlgReceiveFloatArrayData))]
    static void OnFloatArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeParamIndex = 1)] float[] data, int length)
    {
        debuglogReceiveData<float>("float", data, length);
    }

    [MonoPInvokeCallback(typeof(dlgReceiveDoubleArrayData))]
    static void OnDoubleArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8, SizeParamIndex = 1)] double[] data, int length)
    {
        debuglogReceiveData<double>("double", data, length);
    }

    [DllImport("__Internal")]
    private static extern void testSendDataToUnity();

    void Start()
    {
        initCallback(OnText, OnSByteArray, OnByteArray, OnShortArray, OnUShortArray, OnIntArray, OnUIntArray, OnFloatArray, OnDoubleArray);

        sbyteArrayTest(new sbyte[] { 1, 2, 3 }, 3);
        byteArrayTest(new byte[] { 1, 2, 3 }, 3);
        shortArrayTest(new short[] { 1, 2, 3 }, 3);
        ushortArrayTest(new ushort[] { 1, 2, 3 }, 3);
        intArrayTest(new int[] { 1, 2, 3 }, 3);
        uintArrayTest(new uint[] { 1, 2, 3 }, 3);
        floatArrayTest(new float[] { 1, 2, 3 }, 3);
        doubleArrayTest(new double[] { 1, 2, 3 }, 3);

        testSendDataToUnity();
    }

}
