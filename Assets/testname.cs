using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class testname : MonoBehaviour {
    class Test
    {
        public int StaticPer = 5;
        public readonly string ReadOnlyPer = "ReadOnlyPer";
        public const string ConstPer = "ConstPer";

        public string GetPer { get { return "GetPer"; } }
        private string _SetPer;
        public string SetPer { set { _SetPer = value; } }
        public string GetSetPer { get; set; }

        public void Ok()
        {

        }

    }

    private void Start()
    {
        Test p = new Test();
        p.GetType().GetField("StaticPer").SetValue(p, 88);
        Debug.Log(p.GetType().GetField("StaticPer").GetValue(p).ToString());

        //var obj = new Test();
        //var type = obj.GetType();
        //var p = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        //foreach (var item in p)
        //{
        //   Debug.Log(item.Name);
        //}

        //foreach (FieldInfo field in type.GetFields())
        //{
        //    Console.WriteLine(field.Name+"---"+field.GetValue(obj));
        //}
    }
   
}
