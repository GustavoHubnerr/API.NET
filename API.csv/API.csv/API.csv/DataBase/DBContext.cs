using API.csv.DataBase.models;
using System;
using System.Collections.Generic;
using System.IO;

namespace API.csv.DataBase
{
    public class DBContext
    {
        private const string PathName =
            "C:\\Users\\hubne\\Desktop\\5° período\\Paradigmas de programação\\API.NET\\API.csv\\API.csv\\API.csv\\animais.txt";
        private readonly List<Animal> _animais = new();

        public DBContext()
        {
            string[] lines = 
                File.ReadAllLines(PathName);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] coluns = 
                lines[i].Split(';');

                Animal animal = new();
                animal.Id = int.Parse(coluns[0]);
                animal.Name = coluns[1];
                animal.Classification = coluns[2];
                animal.Origin = coluns[3];
                animal.Reproduction = coluns[4];
                animal.Feeding = coluns[5];

                Animals.Add(animal);
            }
        }
        public List<Animal> Animals => _animais;
    }
}
