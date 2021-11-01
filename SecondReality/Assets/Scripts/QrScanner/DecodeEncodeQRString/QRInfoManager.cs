using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QRInfoManager
{
    //обработчик инфы дл€ qr кодов, расшифровка строки, зашифровка.
    // http//rcrjkfr.com/hbjbhjb/1#0;0;0;0;0;0;1;1;1
    // функци€ пробует декодить ссылку, если это сторонн€€ ссылка, то не использует ее.
    public static bool Decode(string str, ref QRInfo qrInfo)
    {

        qrInfo = new QRInfo();
        Debug.Log("Start decode:" + str);
        var parts = str.Split('#',';');
        try
        {
            //TODO
            // придумать более из€щную систему получени€ параметров из ссылки.
            foreach (var item in parts)
            {
                Debug.Log(item);
            }

            qrInfo.URL = parts[0];
            Debug.Log("URL done");
            qrInfo.StartPosition = new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
            Debug.Log("StartPosition done");
            qrInfo.Offset = new Vector3(float.Parse(parts[4]), float.Parse(parts[5]), float.Parse(parts[6]));
            Debug.Log("Offset done");
            qrInfo.Dimensions = new Vector3(float.Parse(parts[7]), float.Parse(parts[8]), float.Parse(parts[9]));
            Debug.Log("Dimensions  done");

            return true;
        }
        catch(Exception error)
        {
            Debug.LogError(error.Message);
            return false;
        }

    }

    public static string Encode(QRInfo qrInfo)
    {
        string encodedInfo = "";
        encodedInfo += qrInfo.URL;
        encodedInfo += "#";
        encodedInfo += qrInfo.StartPosition.x + ";" + qrInfo.StartPosition.y + ";" + qrInfo.StartPosition.z + ";" ;
        encodedInfo += qrInfo.Offset.x + ";" + qrInfo.Offset.y + ";" + qrInfo.Offset.z + ";";
        encodedInfo += qrInfo.Dimensions.x + ";" + qrInfo.Dimensions.y + ";" + qrInfo.Dimensions.z;

        return encodedInfo;
    }
    
}
