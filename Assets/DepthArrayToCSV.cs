using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DepthArrayToCSV{
    static private bool doOnce = false;
    static private int csvCounter = 0; 

    public static void ConvertArrayToCSV(PicoFlexxSensor.Point[] temp_array){
        List<PicoFlexxSensor.Point> pointList = new List<PicoFlexxSensor.Point>();


        for(int i = 0; i<temp_array.Length;i++){
            float distance = temp_array[i].distance;
            if(distance != 0){
                PicoFlexxSensor.Point point = temp_array[i];
                pointList.Add(point);
            }
        }

        string path = string.Format("C:\\Users\\schon\\Desktop\\CSV\\csv{0}.csv",csvCounter);
        using(System.IO.StreamWriter file = new System.IO.StreamWriter(path,true)){
            for(int i = 0; i<pointList.Count;i++){
                PicoFlexxSensor.Point point = pointList[i];

                Vector2 position = point.position;
                float distance = point.distance;

                file.WriteLine(position.x/1000+", "+position.y/1000+", "+distance);
            }
        }
        csvCounter++;
    }
}
