using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Movement
{
    public class NameGlossary
    {
        static public string nameName = "Name";
        static public string descriptionName = "Description";
        static public string effectName = "Effect";
        static public string flavorName = "Flavor_Text";
        static public string summaryName = "Summary";
        static public string[] directionNames = { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
        static public string[] distanceNames = { "N_Distance", "NE_Distance", "E_Distance", "SE_Distance", "S_Distance",
                "SW_Distance", "W_Distance", "NW_Distance" };

    }

    public string name;
    public string description = "";
    public string effect;
    public string summary;
    public string flavor;
    public bool[] directions = new bool[8];
    public int[] distances = new int[8];
}

public class Technique
{
    public class NameGlossary
    {
        static public string nameName = "Name";
        static public string descriptionName = "Description";
        static public string effectName = "Effect";
        static public string flavorName = "Flavor_Text";
        static public string summaryName = "Summary";
        static public string rangeName = "Range";
        static public string[] directionNames = { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
        static public string[] damagesNames = { "N_Damage", "NE_Damage", "E_Damage", "SE_Damage", "S_Damage", "SW_Damage", "W_Damage", "NW_Damage" };
        static public string[] precisionNames = { "N_Precision", "NE_Precision", "E_Precision", "SE_Precision", "S_Precision", "SW_Precision", "W_Precision", "NW_Precision" };
        //public string[] damagesNames = { "", "", "", "", "","","","" };


    }

    public string name;
    public string description;
    public string effect;
    public string summary;
    public string flavor;
    public int range;
    public bool[] directions = new bool[8];
    public int[] damages = new int[8];
    public int[] precision = new int[8];
}


public static class LoadedData {

    public static List<Technique> techniques = new List<Technique>();
    public static List<Movement> movements = new List<Movement>();


    private static void LoadTechniques(string fileAddress)
    {
        techniques.Clear();

        string[] unsplit = CustomSplit(File.ReadAllText(System.IO.Path.ChangeExtension(fileAddress, ".csv")), '$');
        Dictionary<string, int> nameIndexes = new Dictionary<string, int>();
        string[][] lines = new string[unsplit.Length][];

        for (int d = 0; d < unsplit.Length; d++)
        {
            //lines[d] = CSVParser.Split(unsplit[d]);
            //lines[d] = unsplit[d].Split();
            lines[d] = CustomSplit(unsplit[d], ',');
            //System.Diagnostics.Debug.WriteLine(unsplit[d] + "\n\n\n");
            //System.Diagnostics.Debug.WriteLine(lines[d].Length + "\n\n\n");
        }


        for (int n = 0; n < lines[0].Length; n++)
        {
            if (!nameIndexes.ContainsKey(lines[0][n]))
            {
                nameIndexes.Add(lines[0][n].Trim(), n); ;
                //System.Diagnostics.Debug.WriteLine(lines[0][n]);
            }
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i];

            Technique t = new Technique();

            if (nameIndexes.ContainsKey(Technique.NameGlossary.nameName))
            {
                t.name = values[nameIndexes[Technique.NameGlossary.nameName]];
            }

            if (nameIndexes.ContainsKey(Technique.NameGlossary.descriptionName))
            {
                string desc = values[nameIndexes[Technique.NameGlossary.descriptionName]];
                t.description = desc;
            }

            if (nameIndexes.ContainsKey(Technique.NameGlossary.effectName))
            {
                t.effect = values[nameIndexes[Technique.NameGlossary.effectName]];
            }

            if (nameIndexes.ContainsKey(Technique.NameGlossary.summaryName))
            {
                t.summary = values[nameIndexes[Technique.NameGlossary.summaryName]];
            }

            if (nameIndexes.ContainsKey(Technique.NameGlossary.flavorName))
            {
                t.flavor = values[nameIndexes[Technique.NameGlossary.flavorName]];
            }

            if (nameIndexes.ContainsKey(Technique.NameGlossary.rangeName))
            {
                //System.Diagnostics.Debug.WriteLine(values[nameIndexes[Technique.NameGlossary.rangeName]]);
                t.range = int.Parse(values[nameIndexes[Technique.NameGlossary.rangeName]]);
            }

            for (int dir = 0; dir < 8; dir++)
            {
                //if (nameIndexes.ContainsKey(Technique.NameGlossary.damagesNames[dir]))
                if (nameIndexes.ContainsKey(Technique.NameGlossary.directionNames[dir]))
                {
                    //System.Diagnostics.Debug.WriteLine(int.Parse(values[nameIndexes[Technique.NameGlossary.directionNames[dir]]]) 
                    //    + " " + (values[nameIndexes[Technique.NameGlossary.directionNames[dir]]]));
                    //System.Diagnostics.Debug.WriteLine(values[nameIndexes[Technique.NameGlossary.directionNames[dir]]]);
                    if (string.IsNullOrWhiteSpace(values[nameIndexes[Technique.NameGlossary.directionNames[dir]]]))
                    {
                        t.directions[dir] = false;
                    }
                    else
                        t.directions[dir] = (int.Parse(values[nameIndexes[Technique.NameGlossary.directionNames[dir]]]) != 0 ? true : false);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(Technique.NameGlossary.directionNames[dir] + " not found.");
                }

                if (nameIndexes.ContainsKey(Technique.NameGlossary.damagesNames[dir]))
                {
                    t.damages[dir] = int.Parse(values[nameIndexes[Technique.NameGlossary.damagesNames[dir]]]);
                }

                if (nameIndexes.ContainsKey(Technique.NameGlossary.precisionNames[dir]))
                {
                    t.precision[dir] = int.Parse(values[nameIndexes[Technique.NameGlossary.precisionNames[dir]]]);
                }
            }

            techniques.Add(t);
        }
    }

    private static void LoadMovements(string fileAddress)
    {
        movements.Clear();

        //string[][] lines = readExcel(fileAddress);

        //string[] unsplit = File.ReadAllLines(System.IO.Path.ChangeExtension(fileAddress, ".csv"));
        string[] unsplit = CustomSplit(File.ReadAllText(System.IO.Path.ChangeExtension(fileAddress, ".csv")), '$');
        Dictionary<string, int> nameIndexes = new Dictionary<string, int>();
        //string[] headers = lines[0].Split(';');
        string[][] lines = new string[unsplit.Length][];
        //Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int d = 0; d < unsplit.Length; d++)
        {
            //lines[d] = CSVParser.Split(unsplit[d]);
            //lines[d] = unsplit[d].Split();
            lines[d] = CustomSplit(unsplit[d], ',');
        }


        for (int n = 0; n < lines[0].Length; n++)
        {
            if (!nameIndexes.ContainsKey(lines[0][n]))
            {
                //System.Diagnostics.Debug.WriteLine(string.Format("\"{0}\"", lines[0][n]));
                nameIndexes.Add(lines[0][n].Trim(), n);
            }
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i];

            Movement t = new Movement();

            if (nameIndexes.ContainsKey(Movement.NameGlossary.nameName))
            {
                t.name = values[nameIndexes[Movement.NameGlossary.nameName]];

                if (string.IsNullOrEmpty(t.name))
                {
                    continue;
                }
            }

            if (nameIndexes.ContainsKey(Movement.NameGlossary.descriptionName))
            {
                string desc = values[nameIndexes[Movement.NameGlossary.descriptionName]];
                t.description = desc;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Description not found.");
            }

            if (nameIndexes.ContainsKey(Movement.NameGlossary.effectName))
            {
                t.effect = values[nameIndexes[Movement.NameGlossary.effectName]];
            }

            if (nameIndexes.ContainsKey(Movement.NameGlossary.summaryName))
            {
                t.summary = values[nameIndexes[Movement.NameGlossary.summaryName]];
            }

            if (nameIndexes.ContainsKey(Movement.NameGlossary.flavorName))
            {
                t.flavor = values[nameIndexes[Movement.NameGlossary.flavorName]];
            }

            for (int dir = 0; dir < 8; dir++)
            {
                //if (nameIndexes.ContainsKey(Movement.NameGlossary.distanceNames[dir]))
                if (nameIndexes.ContainsKey(Movement.NameGlossary.directionNames[dir]))
                {
                    t.directions[dir] = (int.Parse(values[nameIndexes[Movement.NameGlossary.directionNames[dir]]]) != 0 ? true : false);
                }

                if (nameIndexes.ContainsKey(Movement.NameGlossary.distanceNames[dir]))
                {
                    string d = values[nameIndexes[Movement.NameGlossary.distanceNames[dir]]];

                    if (string.IsNullOrEmpty(d))
                        d = "0";

                    t.distances[dir] = int.Parse(d);
                }
            }

            movements.Add(t);
        }
    }

    private static string[] CustomSplit(string str, char delimiter)
    {
        List<string> output = new List<string>();
        int a = 0, n = 0;
        bool splitting = true;

        for (n = 0; n < str.Length; n++)
        {
            if (splitting)
            {
                if (str[n] == delimiter)
                {
                    output.Add(str.Substring(a, n - a));
                    a = n + 1;
                }
                else if (str[n] == '\"')
                {
                    splitting = false;
                }
            }
            else if (str[n] == '\"')
            {
                splitting = true;
            }
        }

        if (a + 1 != n)
        {
            output.Add(str.Substring(a, n - a));
        }

        return output.ToArray();
    }

}
