using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace AllergyClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var allergies = new Allergies("William");

            allergies.AddAllergy("Cats");
            allergies.AddAllergy("Eggs");
            allergies.AddAllergy("Shellfish");

            Console.WriteLine(allergies.ToString());
        }
    }

    public enum Allergen
    {
        Eggs = 1,
        Peanuts = 2,
        Shellfish = 4,
        Strawberries = 8,
        Tomatoes = 16,
        Chocolate = 32,
        Pollen = 64,
        Cats = 128
    }

    public class Allergies
    {
        public Allergies(string name)
        {
            this.Name = name;
            Score = 0;
            _allergenList = new List<int>();
        }

        public Allergies(string name, int score)
        {
            this.Name = name;
            Score = score;

            PopulateAllergenListFromScore(score);
        }

        public Allergies(string name, string allergensSpaceDelimited)
        {
            this.Name = name;
            _allergenList = AllergensSpaceDelimitedToIntList(allergensSpaceDelimited);

            CalculateAllergenScore();
        }

        public string Name { get; private set; }
        public int Score { get; private set; }
        private List<int> _allergenList;

        public override string ToString()
        {
            string returnString = "";

            if (_allergenList.Count == 0)
            {
                returnString = Name + " has no allergies!";
            }

            if (_allergenList.Count == 1)
            {
                returnString = Name + " is allergic to " + (Allergen)_allergenList[0] + ".";
            }

            if (_allergenList.Count > 1)
            {
                returnString = Name + " is allergic to ";

                for (int i = 0; i < _allergenList.Count; i++)
                {
                    if (i == _allergenList.Count - 1) returnString = returnString.Substring(0, returnString.Length - 2) + " and ";

                    returnString += (Allergen)_allergenList[i] + ", ";
                }

                returnString = returnString.Substring(0, returnString.Length - 2);

                returnString += ".";
            }

            return returnString;
        }

        public List<int> AllergensSpaceDelimitedToIntList(string allergensSpaceDelimited)
        {
            var intList = new List<int>();

            var allergenArray = allergensSpaceDelimited.Split(' ');

            foreach (var item in allergenArray)
            {
                intList.Add((int)Enum.Parse(typeof(Allergen), item));
            }

            intList.Sort();

            return intList;
        }
        
        public void PopulateAllergenListFromScore(int score)
        {
            var intList = new List<int>();

            if (score >= 128) { intList.Add(128); score -= 128; }
            if (score >= 64) { intList.Add(64); score -= 64; }
            if (score >= 32) { intList.Add(32); score -= 32; }
            if (score >= 16) { intList.Add(16); score -= 16; }
            if (score >= 8) { intList.Add(8); score -= 8; }
            if (score >= 4) { intList.Add(4); score -= 4; }
            if (score >= 2) { intList.Add(2); score -= 2; }
            if (score >= 1) { intList.Add(1); }

            _allergenList = intList;

            _allergenList.Sort();
        }

        public void CalculateAllergenScore()
        {
            int score = 0;

            foreach (var item in _allergenList)
            {
                score += item;
            }

            Score = score;
        }

        public bool IsAllergicTo(string allergen)
        {
            return _allergenList.Contains((int)Enum.Parse(typeof(Allergen), allergen));
        }

        public bool IsAllergicTo(Allergen allergen)
        {
            return _allergenList.Contains((int)allergen);
        }

        public void AddAllergy(string allergen)
        {
            if (!_allergenList.Contains((int)Enum.Parse(typeof(Allergen), allergen)))
            {
                _allergenList.Add((int)Enum.Parse(typeof(Allergen), allergen));
                CalculateAllergenScore();
            }

            _allergenList.Sort();
        }

        public void AddAllergy(Allergen allergen)
        {
            if (!_allergenList.Contains((int)allergen))
            {
                _allergenList.Add((int)allergen);
                CalculateAllergenScore();
            }

            _allergenList.Sort();
        }

        public void DeleteAllergy(string allergen)
        {
            if (_allergenList.Contains((int)Enum.Parse(typeof(Allergen), allergen)))
            {
                _allergenList.Remove((int)Enum.Parse(typeof(Allergen), allergen));
                CalculateAllergenScore();
            }

            _allergenList.Sort();
        }

        public void DeleteAllergy(Allergen allergen)
        {
            if (_allergenList.Contains((int)allergen))
            {
                _allergenList.Remove((int)allergen);
                CalculateAllergenScore();
            }

            _allergenList.Sort();
        }
    }
}