using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// обновленный класс для хранения инфы из кода с поддержкой json
[Serializable]
public class QrInfoSubstitution
{
    //ссылка на контент на сервере
    public string url;
    //версия контента
    public int ID;

    //public Vector3 Offset;
    //public Vector3 Dimensions;

    public override string ToString() 
    {
        return "url: " + url + "\nID:" + ID;
    }
}


