using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DepthArrayToCSV{
    static private bool doOnce = false;
    static private int csvCounter = 0; 

    public static void ConvertArrayToCSV(PicoFlexxSensor.Point[] temp_array){
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        List<PicoFlexxSensor.Point> pointList = new List<PicoFlexxSensor.Point>();


        for(int i = 0; i<temp_array.Length;i++){
            float distance = temp_array[i].distance;
            if(distance != 0){
                PicoFlexxSensor.Point point = temp_array[i];
                pointList.Add(point);
            }
        }

        string path = string.Format("C:\\Users\\Kasper\\Desktop\\CSV\\csv{0}.csv",csvCounter);
        using(System.IO.StreamWriter file = new System.IO.StreamWriter(path,true)){
            for(int i = 0; i<pointList.Count;i++){
                PicoFlexxSensor.Point point = pointList[i];

                //point.normal;
                //Vector2 position = point.position;
                Vector3 impactPoint = point.impactPoint;
                float distance = point.distance;

                file.WriteLine(impactPoint.x+", "+impactPoint.y+", "+distance);
            }
        }
        csvCounter++;
    }
}
